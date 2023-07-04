using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using UniversalNumerics.Networking.Layers;

// ReSharper disable once CheckNamespace
namespace UniversalNumerics.Networking {
    public static class Genetics {
        private class Agent {
            public MLP internalNetwork;
            public double fitness;

            public Agent(MLP networkTemplate) {
                internalNetwork = new MLP(networkTemplate);
            }
        }

        private static Agent[] generatePopulation(int populationCount, MLP networkTemplate) {
            Agent[] Population = new Agent[populationCount];
            for (int i = 0; i < populationCount; i++) {
                Population[i] = new Agent(networkTemplate);
            }

            return Population;
        }

        private static Agent[] calculateFitness(Agent[] Agents, double[][][] X, double[][][] y) {
            foreach (Agent Agent in Agents) {
                double MSE = 0;
                int Count = 0;
                
                for (int i = 0; i < y.Length; i++) {
                    double[][] Row = X[i];
                    double[][] Expected = y[i];
                    double[][] Predicted = Agent.internalNetwork.Forward(Row);
                    
                    MSE += Loss.MSE(Predicted, Expected);
                }

                MSE /= Count;

                Agent.fitness = MSE;
            }

            return Agents;
        }

        private static Agent[] naturalSelection(Agent[] Agents) {
            Agent[] Sorted = Agents.OrderBy(Agent => Agent.fitness).ToArray();
            int Cutoff = (int)(0.2 * Agents.Length);
            Agent[] Final = new Agent[Cutoff+1];

            for (int i = 0; i < Agents.Length; i++) {
                if (i <= Cutoff) {
                    Final[i] = Sorted[i];
                }
                else {
                    break;
                }
            }

            return Final;
        }

        [Pure]
        private static double[] generateGenes(MLP Network) {
            return (
                from Layer in Network.Layers
                from WeightArray in Layer.Weights
                from Weight in WeightArray
                select Weight
            ).ToArray();
        }

        private static void fillWeights(MLP Network, double[] Genes) {
            int NextStart = 0;
            foreach (Dense Layer in Network.Layers) {
                double[][] NewWeights = new double[Layer.OutputCount][];
                
                for (int i = 0; i < Layer.OutputCount; i++) {
                    double[] NewSubWeights = new double[Layer.InputCount];
                    
                    for (int j = 0; j < Layer.InputCount; j++) {
                        NewSubWeights[j] = Genes[NextStart++];
                    }

                    NewWeights[i] = NewSubWeights;
                }

                Layer.Weights = NewWeights;
            }
        }

        private static double[] spliceGenes(double[] A, double[] B, int Split) {
            double[] NewGenes = new double[A.Length];

            for (int i = 0; i < A.Length; i++) {
                if (i <= Split) {
                    NewGenes[i] = A[i];
                }
                else {
                    NewGenes[i] = B[i];
                }
            }

            return NewGenes;
        }

        private static Agent[] crossoverAgents(Agent[] Agents, MLP templateNetwork, int populationCount) {
            List<Agent> Offspring = Agents.ToList();
            System.Random Random = new System.Random();

            for (int i = 0; i < populationCount; i++) {
                Agent Parent1 = Agents[Random.Next(0, Agents.Length)];
                Agent Parent2 = Agents[Random.Next(0, Agents.Length)];

                Agent Child = new(templateNetwork);

                double[] Genes1 = generateGenes(Parent1.internalNetwork);
                double[] Genes2 = generateGenes(Parent2.internalNetwork);

                int Split = Random.Next(0, Genes1.Length-1);

                double[] ChildGenes = spliceGenes(Genes1, Genes2, Split);

                fillWeights(Child.internalNetwork, ChildGenes);

                Offspring.Add(Child);
            }

            return Offspring.ToArray();
        }

        private static Agent[] mutateAgents(Agent[] Agents) {
            System.Random Random = new System.Random();
            foreach (Agent Agent in Agents) {
                if (Random.NextDouble() <= 0.1) {
                    double[] Genes = generateGenes(Agent.internalNetwork);
                    Genes[Random.Next(0, Genes.Length - 1)] = Random.NextDouble();
                    fillWeights(Agent.internalNetwork, Genes);
                }
            }
            return Agents;
        }

        public static MLP train(MLP templateNetwork, double threshhold, int populationCount, int generationCount, double[][][] X, double[][][] y) {
            Agent[] Agents = generatePopulation(populationCount, templateNetwork);
            for (int i = 0; i < generationCount; i++) {
                Agents = calculateFitness(Agents, X, y);
                Agents = naturalSelection(Agents);
                Agents = crossoverAgents(Agents, templateNetwork, populationCount);
                Agents = mutateAgents(Agents);
                Agents = calculateFitness(Agents, X, y);
            }
            return Agents[0].internalNetwork;
        }
    }
}
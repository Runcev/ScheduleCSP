using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Schedule.Genetic.Genetic
{
	public class GeneticAlgorithm<A>
	{
		private readonly int _individualLength;
		private readonly List<A> _alphabet;
		private readonly double _probability;
		private int _itCount = 0;

		private readonly Random _random = new Random();

		public GeneticAlgorithm(int individualLength, List<A> finiteAlphabet, double mutationProbability)
		{
			_individualLength = individualLength;
			_alphabet = new List<A>(finiteAlphabet);
			_probability = mutationProbability;

		}

		public Individual<A> Algorithm(List<Individual<A>> initPopulation,
			IFitnessFn<A> fitnessFn, int maxIterations)
		{
			bool GoalTest(Individual<A> s) => _itCount >= maxIterations;
			return Algorithm(initPopulation, fitnessFn, GoalTest);
		}

		public Individual<A> Algorithm(List<Individual<A>> initPopulation, IFitnessFn<A> fitnessFn,
			Predicate<Individual<A>> goalTest)
		{
			Individual<A> bestIndividual = null;

			var population = new List<Individual<A>>(initPopulation);

			do
			{
				_itCount++;
				population = NextGeneration(population, fitnessFn);
				bestIndividual = RetrieveBestIndividual(population, fitnessFn);
			} while (!goalTest.Invoke(bestIndividual));

			return bestIndividual;
		}

		public Individual<A> RetrieveBestIndividual(List<Individual<A>> population, IFitnessFn<A> fitnessFn)
		{
			Individual<A> bestIndividual = null;
			var bestSoFarFValue = double.NegativeInfinity;

			foreach (var individual in population)
			{
				var fValue = fitnessFn.Apply(individual);
				if (fValue > bestSoFarFValue)
				{
					bestIndividual = individual;
					bestSoFarFValue = fValue;
				}
			}

			return bestIndividual;
		}

		public List<Individual<A>> NextGeneration(List<Individual<A>> population, IFitnessFn<A> fitnessFn)
		{
			var newPopulation = new List<Individual<A>>(population.Count);
			foreach (var t in population)
			{
				var x = RandomSelection(population, fitnessFn);
				var y = RandomSelection(population, fitnessFn);
				var child = Reproduce(x, y);

				if (_random.NextDouble() <= _probability)
				{
					child = Mutate(child);
				}

				newPopulation.Add(child);
			}

			return newPopulation;
		}

		public Individual<A> RandomSelection(List<Individual<A>> population, IFitnessFn<A> fitnessFn)
		{
			var selected = population.ElementAt(population.Count - 1);

			double[] fValues = new double[population.Count];

			for (var i = 0; i < population.Count; i++)
			{
				fValues[i] = fitnessFn.Apply(population.ElementAt(i));
			}

			fValues = Normalize(fValues);
			var prob = _random.NextDouble();
			var totalSoFar = 0.0;
			for (var i = 0; i < fValues.Length; i++)
			{
				totalSoFar += fValues[i];
				if (prob <= totalSoFar)
				{
					selected = population.ElementAt(i);
					break;
				}
			}

			return selected;
		}

		public Individual<A> Reproduce(Individual<A> x, Individual<A> y)
		{

			var c = RandomOffset(_individualLength);

			var childRepresentation = new List<A>();

				childRepresentation.AddRange(x.Representation.GetRange(0, c));
				childRepresentation.AddRange(y.Representation.GetRange(c, _individualLength));


			return new Individual<A>(childRepresentation);
		}

		public Individual<A> Mutate(Individual<A> child)
		{
			var mutateOffset = RandomOffset(_individualLength);
			var alphaOffset = RandomOffset(_alphabet.Count);

			var mutatedRepresentation = new List<A>(child.Representation);

			mutatedRepresentation.Insert(mutateOffset, _alphabet.ElementAt(alphaOffset));

			return new Individual<A>(mutatedRepresentation);
		}

		private double[] Normalize(double[] fValues)
		{
			var len = fValues.Length;
			var total = fValues.Sum();

			if (total != 0)
			{
				for (var i = 0; i < len; i++)
				{
					fValues[i] /= total;
				}
			}

			return fValues;
		}

		private int RandomOffset(int length)
		{
			return _random.Next(length);
		}
	}
}
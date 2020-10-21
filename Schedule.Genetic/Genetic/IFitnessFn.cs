namespace Schedule.Genetic.Genetic
{
    public interface IFitnessFn<A>
    {
        double Apply(Individual<A> individual);
    }
}
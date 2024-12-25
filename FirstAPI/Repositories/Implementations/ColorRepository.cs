namespace FirstAPI.Repositories.Implementations
{
    public class ColorRepository : Repository<Color>, IColorRepository
    {
        public ColorRepository(AppDbContext context) : base(context) { }
    }
}

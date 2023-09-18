using MyMapper;

namespace TestMapping
{
    internal class Program
    {
        static void Main(string[] args)
        {
            User user = new()
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
            };

            UserDto dto = new();
            Mapper<User, UserDto> myMapper = new();
            dto = myMapper.MappingModels(user);
            Console.WriteLine(dto.Name);
            Console.ReadKey();
        }
    }

    public class UserDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
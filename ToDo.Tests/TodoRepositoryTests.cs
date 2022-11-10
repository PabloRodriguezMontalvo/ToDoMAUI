
using TodoApi.Models;
using Bogus;

using Xunit;

namespace ToDo.Tests
{
    public class TodoRepositoryTests
    {
        [Fact]
        public void Add_ShouldInsertANewRecordIntoList()
        {

            var sut = new TodoRepository();
            var faker = new Faker<TodoItem>()
            .RuleFor(x => x.Key, f => Guid.NewGuid().ToString())
            .RuleFor(x => x.Name, f => f.Name.FirstName())
            .RuleFor(x => x.IsComplete, f => f.IndexFaker == 0 ? true : false);

            var entity = faker.Generate(1)[0];

            sut.Add(entity);
            //Debemos tener en cuenta el registro que crea por defecto
            Assert.Equal(2,sut.GetAll().Count());
            var olditems = sut.GetAll().Count();
            sut.Remove(entity.Key);
            var allItems = sut.GetAll().Count();
            Assert.NotEqual(olditems, allItems);
        }
        [Fact]
        public void Remove_ShouldRemoveRecordIntoList()
        {

            var sut = new TodoRepository();
            var faker = new Faker<TodoItem>()
            .RuleFor(x => x.Key, f => Guid.NewGuid().ToString())
            .RuleFor(x => x.Name, f => f.Name.FirstName())
            .RuleFor(x => x.IsComplete, f => f.IndexFaker == 0 ? true : false);

            var entity = faker.Generate(1)[0];

            sut.Add(entity);
            //Debemos tener en cuenta el registro que crea por defecto
            Assert.Equal(2, sut.GetAll().Count());

            var actual = sut.GetAll().First();
            Assert.Equal(entity.Name, actual.Name);
        }
        [Fact]
        public void Update_ShouldChangeNameIntoList()
        {

            var sut = new TodoRepository();
            var faker = new Faker<TodoItem>()
            .RuleFor(x => x.Key, f => Guid.NewGuid().ToString())
            .RuleFor(x => x.Name, f => f.Name.FirstName())
            .RuleFor(x => x.IsComplete, f => f.IndexFaker == 0 ? true : false);

            var entity = faker.Generate(1)[0];

            sut.Add(entity);

            var actual = sut.GetAll().First().Name;

            entity.Name = "OtherThing";
            sut.Update(entity);
            var entity2 = sut.GetAll().First();
            //Debemos tener en cuenta el registro que crea por defecto

            Assert.Equal(2,sut.GetAll().Count());
            Assert.NotEqual(actual, entity2.Name);
            Assert.Equal(entity2.Key, entity.Key);

        }
    }
}

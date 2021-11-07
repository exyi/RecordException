using System;
using RecordExceptions;
using Xunit;

namespace Something.Test
{

    // public record MissingIdException(int id): ExceptionRecords.RecordException($"Id {id} is missing") { }
    public record MissingIdException(int id): RecordException
    {
        public override string Message => $"Id {id} is missing";
    }
    public record MissingIdException2(int id): RecordException;
    public record SomethingBad(Exception InnerException): RecordException(InnerException);
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // RecordExceptions.RecordException.TestMethod();

            var msg = Assert.Throws<MissingIdException>(() => {
                throw new MissingIdException(123);
                return 0;
            });
            Assert.Equal("Id 123 is missing", msg.Message);
            Assert.StartsWith("Something.Test.MissingIdException: Id 123 is missing", msg.ToString());
            Assert.Contains("MissingIdException properties: { id = 123 }", msg.ToString());

            Console.WriteLine(msg.ToString());

            var msg2 = msg with { id = 0 };

            Assert.Equal("Id 0 is missing", msg2.Message);

        }

         [Fact]
        public void Test2()
        {
            // RecordExceptions.RecordException.TestMethod();

            var msg = Assert.Throws<MissingIdException2>(() => {
                throw new MissingIdException2(123);
                return 0;
            });
            Assert.Equal("id = 123", msg.Message);
            Assert.StartsWith("Something.Test.MissingIdException2: id = 123", msg.ToString());
            Assert.Contains("MissingIdException2 properties: { id = 123 }", msg.ToString());

            Console.WriteLine(msg.ToString());

            var msg2 = msg with { id = 0 };

            Assert.Equal("id = 0", msg2.Message);

        }
    }
}

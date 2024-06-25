using NetMessaging.Domain.Entities;
using NetMessaging.Domain.Exception;
using NetMessaging.Domain.Ports;
using NetMessaging.Domain.Services;
using NSubstitute;

namespace NetMessaging.Domain.Tests
{
    public class GenerateTurnTests
    {
        readonly ITurnRepository _repository = default!;        
        readonly GenerateTurnService _service = default!;

        public GenerateTurnTests()
        {            
            _repository = Substitute.For<ITurnRepository>();
            _service = new GenerateTurnService(_repository);
        }        

        [Fact]
        public async void GenerateTurns_WithWrongStartDate()
        {
            try
            {
                TurnGenerate turnGenerate = new(DateOnly.FromDateTime(DateTime.Now.AddDays(-1)), DateOnly.FromDateTime(DateTime.Now), 1);
                await _service.GenerateTurns(turnGenerate);
                Assert.Fail("It shouldn't get to this point");
            }
            catch (CoreBusinessException cbe)
            {
                Assert.True(cbe.Message.Equals("Start date must be equal to or greater than the current date"));
            }
        }

        [Fact]
        public async void GenerateTurns_WithWrongEndDate()
        {
            try
            {
                TurnGenerate turnGenerate = new(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(-1)), 1);
                await _service.GenerateTurns(turnGenerate);
                Assert.Fail("It shouldn't get to this point");
            }
            catch (CoreBusinessException cbe)
            {
                Assert.True(cbe.Message.Equals("End date must be equal to or greater than the start date"));
            }
        }
    }
}
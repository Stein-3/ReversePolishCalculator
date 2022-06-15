using Microsoft.AspNetCore.Mvc;
using Calculator;

namespace RPNCalculator.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RPNCalculatorController : ControllerBase
    {
        private readonly ICalculator calculator;

        public RPNCalculatorController(ICalculator calculator) => this.calculator = calculator;

        [HttpGet(Name = "AllCommand")]
        public GetAllCommandResponse GetAllCommand()
            => new()
            {
                Commands = calculator.GetAllCommand().Split("\n")
            };

        [HttpPost("CommandExecution")]
        public ActionResult<ExecutionResponse> PostCommandExecution(Command command)
        {
            try
            {
                var result = calculator.CallCommand(command.Name, null);
                return new ExecutionResponse
                {
                    Result = (result?.ToString()) ?? ""
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        [HttpPost("FormulaPush")]
        public ActionResult<PushResponse> PostFormulaPush(PushCommand pushCommand)
        {
            try
            {
                calculator.Push(pushCommand.Formula);
                return new PushResponse
                {
                    Stack = calculator.DisplayStack()
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class Command
    {
        public string Name { get; set; } = "";
    }

    public class PushCommand
    {
        public string Formula { get; set; } = "";
    }

    public class PushResponse
    {
        public string Stack { get; set; } = "";
    }

    public class ExecutionResponse
    {
        public string Result { get; set; } = "";
    }

    public class GetAllCommandResponse
    {
        public IEnumerable<string> Commands { get; set; } = new List<string>();
    }
}
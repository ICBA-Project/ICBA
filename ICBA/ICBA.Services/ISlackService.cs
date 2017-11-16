using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICBA.Services.SlackService;

namespace ICBA.Services
{
    public interface ISlackService
    {
        void PostMessage(string text, string username = "ICBA-Sensors-Information", string channel = "#sensorsfeedback");

        void PostWelcomingMessage();
    }
}

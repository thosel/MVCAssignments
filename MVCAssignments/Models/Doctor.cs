namespace MVCAssignments.Models
{
    public class Doctor
    {
        public static string FeverCheck(float temperature, string temperatureUnit)
        {
            string diagnosticMessage = "The diagnose is that you're healthy as a horse.";

            switch (temperatureUnit)
            {
                case "celsius":
                    if (temperature < 35.0)
                    {
                        diagnosticMessage = "To be cool is one thing but you've got hypothermia man!";
                    }
                    else if (temperature > 37.5)
                    {
                        diagnosticMessage = "You're a hot potato that I'll pass right into bed. You've got fever man!";
                    }
                    break;
                case "fahrenheit":
                    if (temperature < 95.0)
                    {
                        diagnosticMessage = "To be cool is one thing but you've got hypothermia man!";
                    }
                    else if (temperature > 99.5)
                    {
                        diagnosticMessage = "You're a hot potato that I'll pass right into bed. You've got fever man!";
                    }
                    break;
                default:
                    break;
            }

            return diagnosticMessage;
        }
    }
}

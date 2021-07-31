public class Texts
{
    public static string get(string lang, string name)
    {
        switch (lang)
        {
            case "eng":
                switch (name)
                {
                    case GlobalConstants.textStateGone:  return "Go to";
                    case GlobalConstants.textStateWalking: return "Walking";
                    case GlobalConstants.textStateExtract: return "Mines";
                    case GlobalConstants.textStateSit:   return "Sitting";
                    case GlobalConstants.textStateGive:  return "Puts";
                    case GlobalConstants.textStateWait:  return "Waiting";

                    default:
                        return "NoNe";
                }

            case "ru":
                switch (name)
                {
                    case GlobalConstants.textStateGone: return "Идет к";
                    case GlobalConstants.textStateWalking:  return "Гуляет";
                    case GlobalConstants.textStateExtract:  return "Добывает";
                    case GlobalConstants.textStateSit:   return "Сидит";
                    case GlobalConstants.textStateGive: return "Выкладывает";
                    case GlobalConstants.textStateWait: return "Ждет";

                    default:
                        return "NoNe";
                }

            default:
                return "NoNe";
        }
    }
}
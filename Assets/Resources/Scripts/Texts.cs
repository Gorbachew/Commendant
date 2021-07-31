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
                    case GlobalConstants.textStateGone: return "���� �";
                    case GlobalConstants.textStateWalking:  return "������";
                    case GlobalConstants.textStateExtract:  return "��������";
                    case GlobalConstants.textStateSit:   return "�����";
                    case GlobalConstants.textStateGive: return "�����������";
                    case GlobalConstants.textStateWait: return "����";

                    default:
                        return "NoNe";
                }

            default:
                return "NoNe";
        }
    }
}
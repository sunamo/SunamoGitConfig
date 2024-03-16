namespace SunamoGitConfig
{
    public class ThrowEx
    {
        public static void NotImplementedCase(object _case)
        {
            throw new Exception("Not implemented case: " + _case);
        }

        public static void Custom(string v)
        {
            throw new Exception(v);
        }
    }
}

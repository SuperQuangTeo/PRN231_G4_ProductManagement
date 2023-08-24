namespace PRN231_G4_ProductManagement_BE.Utilities
{
    public class Common
    {
        public static bool checkStringsIsNullOrEmpty(string[] strings)
        {
            foreach (string s in strings)
            {
                if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

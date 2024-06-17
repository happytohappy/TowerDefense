// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("m3xnk6kyZRQUgmzJMPB/ZIZzl5BAWXcCW/60gcKqiwvlayd4416heBHvkdUWj6LgW8MNfte0WRpmSeQ3YdNQc2FcV1h71xnXplxQUFBUUVKOHJM3C0mMj+uCpQ10BFYOWDDE3scDjXmDScm+B9j1ta0MyZdLP4vdVWSl17xYnwNTnwNevV68xadN+XHTUF5RYdNQW1PTUFBRzTGaEzyJuikY2/Pw6490Ou7M1XBn4rR5wIkLs9S4V9C8+ShfQqqL/66DHn67QBNmyAeQos81D4t6TfGRgZasXP1vGr/427BeT8JEZqC3BY/2fpG8Yb+pbvaD6yOfOuChnJp3rWbTYa0LbW4FynTjXfc5URAaLWyjUVaNBNtaDfN3PWeKJGAOHFNSUFFQ");
        private static int[] order = new int[] { 2,8,7,7,9,13,7,8,11,12,10,11,13,13,14 };
        private static int key = 81;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}

using System;

namespace src.GameLogic
{
    public static class ArrayExtensions
    {
        /* C# extension method is a static method of a static class, where the "this" modifier is applied to
        the first parameter. The type of the first parameter will be the type that is extended.
        
        Extension methods are additional custom methods which were originally not included w/ the class.
        
        The first parameter of the extension method must be of the type for which the extension method is applicable,
        preceded by the `this` keyword. */
        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
    }
}
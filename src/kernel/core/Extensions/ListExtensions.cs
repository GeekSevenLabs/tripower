// ReSharper disable once CheckNamespace
namespace TriPower;

public static class ListExtensions
{
    extension<T>(List<T> array)
    {
        public bool IsEmpty => array.Count == 0;
        public bool IsNotEmpty => array.Count > 0;
        
    }
}
namespace VB.Infrastructure.Services
{
    public interface ISerializer
    {
        /// <summary>
        /// Convert string to object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objStr"></param>
        /// <returns></returns>
        T DeSerialize<T>(string objStr);

        /// <summary>
        /// Convert Object to String
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        string Serialize(object obj);
    }
}
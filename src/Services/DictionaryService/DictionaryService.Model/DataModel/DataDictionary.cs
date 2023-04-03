namespace Study402Online.DictionaryService.Model.DataModel
{
    /// <summary>
    /// 数据字典
    /// </summary>
    public class DataDictionary
    {
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
    }
}

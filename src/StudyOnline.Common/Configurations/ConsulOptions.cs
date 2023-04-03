namespace Study402Online.Common.Configurations
{
    /// <summary>
    /// Consul 配置
    /// </summary>
    public class ConsulOptions
    {
        /// <summary>
        /// 服务器地址
        /// </summary>
        public Uri ConsulUri { get; set; }

        /// <summary>
        /// 服务地址
        /// </summary>
        public string ServiceAddress { get; set; }

        /// <summary>
        /// 服务端口
        /// </summary>
        public int ServicePort { get; set; }

        /// <summary>
        /// 超时时间
        /// </summary>
        public int TTL { get; set; }

        /// <summary>
        /// 服务名
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 服务 ID
        /// </summary>
        public string ServiceId { get; set; }
    }
}

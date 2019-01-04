namespace Data_Server.Communication {
    public static class Constants {
        /// <summary>
        /// Tempo limite de leitura em microsegundos.
        /// </summary>
        public const int ReceiveTimeOut = 15 * 1000 * 1000;

        /// <summary>
        /// Tempo limite de envio em microsegundos.
        /// </summary>
        public const int SendTimeOut = 15 * 1000 * 1000;

        /// <summary>
        /// Tempo entre cada ping.
        /// </summary>
        public const int PingTime = 15000;
    }
}
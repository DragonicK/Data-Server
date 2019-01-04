using System;

namespace Data_Server.Data {
    /// <summary>
    /// Dados de um usuário banido.
    /// </summary>
    public struct AccountBan {
        /// <summary>
        /// ID do ban (não é o ID do usuário).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Indica que o ban é permanente ou não.
        /// </summary>
        public bool Permanent { get; set; }

        /// <summary>
        /// Indica o estado atual, expirado ou não expirado.
        /// </summary>
        public Expired Expired { get; set; }

        /// <summary>
        /// Data para expiração.
        /// </summary>
        public DateTime ExpireDate { get; set; }
    }
}
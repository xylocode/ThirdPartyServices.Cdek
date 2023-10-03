using System;
using System.Collections.Generic;
using XyloCode.ThirdPartyServices.Cdek.Enums;

namespace XyloCode.ThirdPartyServices.Cdek.General
{
    public class Request
    {
        /// <summary>
        /// Идентификатор запроса в ИС СДЭК
        /// </summary>
        public Guid RequestUuid { get; set; }

        /// <summary>
        /// Тип запроса
        /// Может принимать значения: CREATE, UPDATE, DELETE, AUTH, GET
        /// </summary>
        public RequestType Type { get; set; }

        /// <summary>
        /// Дата и время установки текущего состояния запроса (формат yyyy-MM-dd'T'HH:mm:ssZ)
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Текущее состояние запроса
        /// Может принимать значения:
        /// ACCEPTED - пройдена предварительная валидация и запрос принят
        /// WAITING - запрос ожидает обработки(зависит от выполнения другого запроса)
        /// SUCCESSFUL - запрос обработан успешно
        /// INVALID - запрос обработался с ошибкой
        /// </summary>
        public RequestState State { get; set; }

        /// <summary>
        /// Ошибки, возникшие в ходе выполнения запроса
        /// </summary>
        public List<Error> Errors { get; set; }

        /// <summary>
        /// Предупреждения, возникшие в ходе выполнения запроса
        /// </summary>
        public List<Error> Warnings { get; set; }
    }
}

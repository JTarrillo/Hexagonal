using System;

namespace Sicv1.Domain.Entities
{
    public class User : BaseClass
    {
        public int TYPE_PROFILE { get; set; }
        public string USERNAME { get; set; }
        public string CODE_LIFEMILES { get; set; }
        public string TYPE_SEGMENT{ get; set; }
        ///******************************************************************//
        /// campo(s) para el store procedure: 'SP_USERS_SIGNIN'              //
        public string FULLNAME { get; set; }                                 //
        public string NAMESHORT { get; set; }                                //
        public string PASS { get; set; }                                     //
        public int ESTADO { get; set; }                                      //
        public string LOGO { get; set; }                                     //
        public string ID_COMPANY { get; set; }                                  //
        ///******************************************************************//
        public string PASSWORD { get; set; }
        public string NAME { get; set; }
        public string LASTNAME_P { get; set; }
        public string LASTNAME_M { get; set; }
        public string TYPE_DOCUMENT { get; set; }
        public string DOCUMENT { get; set; }
        public string EMAIL { get; set; }
        public string PHONE1 { get; set; }
        public string PHONE2 { get; set; }
        public string UBIGEO { get; set; }
        public string TOKEN { get; set; }
        public int SESSION { get; set; }
        public DateTime LAST_SESSION { get; set; }
        public string GENDER { get; set; }
        public string ADDRESS { get; set; }
        public string CARD { get; set; }
        public int FK_ROLE { get; set; }

        /// <summary>
        /// SP_USERS_LIST
        /// </summary>
        public string DEPARTAMENT { get; set; }
        public string PROVINCE { get; set; }
        public string DISTRICT { get; set; }
        public string DEPARTAMENT_ID { get; set; }
        public string PROVINCE_ID { get; set; }
        public string DISTRICT_ID { get; set; }
        public string ROL { get; set; }

        public string ROLE { get; set; }
        public string UPDATE_USERNAME { get; set; }
        public string USUARIO_QUE_REGISTRO { get; set; }
        public string USUARIO_QUE_ACTUALIZO { get; set; }

        public byte[] PWD { get; set; }
        public int COMPANY_ID { get; set; }
        public string LASTNAME { get; set; }
        public bool ADDITIONALS_PURPOSES { get; set; }

        public string REGISTERED_BY { get; set; }
        public string UPDATED_BY { get; set; }
        public string BIRTHDAY { get; set; }

        public string COMPANY { get; set; }

    }
}

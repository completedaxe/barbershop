//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace barbershop
{
    using System;
    using System.Collections.Generic;
    
    public partial class record
    {
        public int ID_record { get; set; }
        public int ID_client { get; set; }
        public int ID_master { get; set; }
        public int ID_service { get; set; }
        public int ID_admin { get; set; }
        public Nullable<System.DateTime> datetime { get; set; }
    
        public virtual admin admin { get; set; }
        public virtual client client { get; set; }
        public virtual master master { get; set; }
        public virtual service service { get; set; }
    }
}

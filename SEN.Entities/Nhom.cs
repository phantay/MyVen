
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace SEN.Entities
{

using System;
    using System.Collections.Generic;
    
public partial class Nhom
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Nhom()
    {

        this.ThanhVienNhoms = new HashSet<ThanhVienNhom>();

    }


    public int NhomId { get; set; }

    public string TenNhom { get; set; }

    public string HinhThuc { get; set; }

    public int ThanhVienId { get; set; }

    public System.DateTime ThoiGianThamGia { get; set; }

    public System.DateTime ThoiGianXacNhan { get; set; }

    public System.DateTime ThoiGianRoi { get; set; }

    public int ThongBaoId { get; set; }



    public virtual ThongBao ThongBao { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<ThanhVienNhom> ThanhVienNhoms { get; set; }

}

}

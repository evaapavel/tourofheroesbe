using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace TourOfHeroesBECommon.BusinessObjects
{



    [Table("TH_HERO")]
    public class Hero
    {



        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("NAME")]
        public string Name { get; set; }



    }



}

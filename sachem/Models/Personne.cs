//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace sachem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Personne
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Personne()
        {
            this.CoursSuivi = new HashSet<CoursSuivi>();
            this.EtuProgEtude = new HashSet<EtuProgEtude>();
            this.Groupe = new HashSet<Groupe>();
            this.GroupeEtudiant = new HashSet<GroupeEtudiant>();
            this.Inscription = new HashSet<Inscription>();
            this.Jumelage = new HashSet<Jumelage>();
        }
    
        public int id_Pers { get; set; }
        public Nullable<int> id_Sexe { get; set; }
        public int id_TypeUsag { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string NomUsager { get; set; }
        public string Matricule { get; set; }
        public string MP { get; set; }
        public string Courriel { get; set; }
        public string Telephone { get; set; }
        public Nullable<System.DateTime> DateNais { get; set; }
        public bool Actif { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CoursSuivi> CoursSuivi { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EtuProgEtude> EtuProgEtude { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Groupe> Groupe { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GroupeEtudiant> GroupeEtudiant { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inscription> Inscription { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Jumelage> Jumelage { get; set; }
        public virtual p_Sexe p_Sexe { get; set; }
        public virtual p_TypeUsag p_TypeUsag { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFormsService.Models
{
    public class Form
    {
        public string appVersion { get; set; }
        public string txtFecha { get; set; }
        public string txtCsrCode { get; set; }
        public string txtIDATM { get; set; }
        public string selClient { get; set; }
        public string txtWO { get; set; }
        public string txtSerie { get; set; }
        public string txtContacto { get; set; }
        public string txtParte { get; set; }
        public byte chkProElectrico { get; set; }
        public byte chkVolNoRegulado { get; set; }
        public byte txtFN { get; set; }
        public byte txtFT { get; set; }
        public byte txtNT { get; set; }
        public byte chkNoUps { get; set; }
        public byte chkNoTierraFisica { get; set; }
        public byte chkNoEnergia { get; set; }
        public byte chkProSite { get; set; }
        public byte chkSuciedad { get; set; }
        public byte chkGoteras { get; set; }
        public byte chkPlagas { get; set; }
        public byte chkExpSol { get; set; }
        public byte chkHumedad { get; set; }
        public byte chkMalaIluminacion { get; set; }
        public byte chkNoAA { get; set; }
        public byte chkProOperativo { get; set; }
        public byte chkSinInsumos { get; set; }
        public byte chkSinBilletes { get; set; }
        public byte chkMalaCalidadBilletes { get; set; }
        public byte chkMalaCalidadInsumos { get; set; }
        public byte chkErrorBalance { get; set; }
        public byte chkErrorOperador { get; set; }
        public byte chkCargaIncPapel { get; set; }
        public byte chkCargaIncCaseteras { get; set; }
        public byte chkSupervisor { get; set; }
        public byte chkProVandalismo { get; set; }
        public byte chkProOtros { get; set; }
        public byte chkProFotos { get; set; }
        public string txtComentario { get; set; }
        public byte chkProComms { get; set; }
        public byte selComunicaciones { get; set; }
    }
}
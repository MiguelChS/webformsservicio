using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using WebFormsService.Models;

namespace WebFormsService.Storage
{
    public class TextFile
    {
        public String fileName { get; set; }
        public String path { get; set; }
        public String ext { get; set; }

        public string Read()
        {
            throw new NotImplementedException();
        }

        public void Write(Mail dataToSave)
        {
            if(dataToSave == null && dataToSave.form != null)
            {
                throw new ArgumentNullException();
            }
            List<String> args = new List<string>();
            args.Add("appVersion: " + dataToSave.form.appVersion);
            args.Add("txtFecha: " + dataToSave.form.txtFecha);
            args.Add("txtCsrCode: " + dataToSave.form.txtCsrCode);
            args.Add("txtIDATM: " + dataToSave.form.txtIDATM);
            args.Add("selClient: " + dataToSave.form.selClient);
            args.Add("txtWO: " + dataToSave.form.txtWO);
            args.Add("txtSerie: " + dataToSave.form.txtSerie);
            args.Add("txtContacto: " + dataToSave.form.txtContacto);
            args.Add("txtParte: " + dataToSave.form.txtParte);
            args.Add("chkProElectrico: " + dataToSave.form.chkProElectrico);
            args.Add("chkVolNoRegulado: " + dataToSave.form.chkVolNoRegulado);
            args.Add("txtFN: " + dataToSave.form.txtFN);
            args.Add("txtFT: " + dataToSave.form.txtFT);
            args.Add("txtNT: " + dataToSave.form.txtNT);
            args.Add("chkNoUps: " + dataToSave.form.chkNoUps);
            args.Add("chkNoTierraFisica: " + dataToSave.form.chkNoTierraFisica);
            args.Add("chkNoEnergia: " + dataToSave.form.chkNoEnergia);
            args.Add("chkProSite: " + dataToSave.form.chkProSite);
            args.Add("chkSuciedad: " + dataToSave.form.chkSuciedad);
            args.Add("chkGoteras: " + dataToSave.form.chkGoteras);
            args.Add("chkPlagas: " + dataToSave.form.chkPlagas);
            args.Add("chkExpSol: " + dataToSave.form.chkExpSol);
            args.Add("chkHumedad: " + dataToSave.form.chkHumedad);
            args.Add("chkMalaIluminacion: " + dataToSave.form.chkMalaIluminacion);
            args.Add("chkNoAA: " + dataToSave.form.chkNoAA);
            args.Add("chkProOperativo: " + dataToSave.form.chkProOperativo);
            args.Add("chkSinBilletes: " + dataToSave.form.chkSinBilletes);
            args.Add("chkMalaCalidadBilletes: " + dataToSave.form.chkMalaCalidadBilletes);
            args.Add("chkMalaCalidadInsumos: " + dataToSave.form.chkMalaCalidadInsumos);
            args.Add("chkErrorOperador: " + dataToSave.form.chkErrorOperador);
            args.Add("chkCargaIncPapel: " + dataToSave.form.chkCargaIncPapel);
            args.Add("chkCargaIncCasetera: " + dataToSave.form.chkCargaIncCaseteras);
            args.Add("chkSupervisor: " + dataToSave.form.chkSupervisor);
            args.Add("chkErrorBalanceo: " + dataToSave.form.chkErrorBalance);
            args.Add("chkProVandalismo: " + dataToSave.form.chkProVandalismo);
            args.Add("chkProOtros:" + dataToSave.form.chkProOtros);
            args.Add("chkProFotos: " + dataToSave.form.chkProFotos);
            args.Add("txtComentario:" + dataToSave.form.txtComentario); 

            string p = path + "\\" + fileName + ext;
            System.IO.File.WriteAllLines(p, args, System.Text.Encoding.UTF8);
        }
    }
}
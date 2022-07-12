using FencebirSubeProject.Business;
using FencebirSubeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FencebirSubeProject.Infra
{
    public class EmailHelper
    {
        private async Task<bool> EpostaGonder(string icerik, int subeId)
        {
            try
            {
                var _SubeBS = new SubeBS();
                var epostaGonderimData = await _SubeBS.EpostaGonderimDataGetir(subeId);

                SmtpClient smtpClient = new SmtpClient(epostaGonderimData.GonderilecekEpostaHost, epostaGonderimData.GonderilecekEpostaPort);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(epostaGonderimData.GonderilecekEpostaKullaniciAdi, epostaGonderimData.GonderilecekEpostaSifre);
                smtpClient.EnableSsl = epostaGonderimData.GonderilecekEpostaSsl;

                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                mail.Subject = "Kurumsal Site E-posta";
                mail.Body = icerik;
                mail.From = new MailAddress(epostaGonderimData.GonderilecekEpostaKullaniciAdi, epostaGonderimData.GonderilecekEpostaTanim);
                mail.To.Add(new MailAddress(epostaGonderimData.GonderilecekEpostaKullaniciAdi));

                await smtpClient.SendMailAsync(mail);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> BilgiTalepEpostaGonder(BilgiTalepViewModel model, int subeId)
        {
            KonuTipBS _KonuTipBS = new KonuTipBS();
            var konuTip = await _KonuTipBS.KonuTipDataGetir(model.KonuTipId);

            string icerik = "<h4>Bilgi Talep</h4><br/>" +
                            "<b>Ad Soyad : </b>" + model.AdSoyad + "<br/>" +
                            "<b>E-posta </b>: " + model.Eposta + "<br/>" +
                            "<b>Telefon </b>: " + model.Telefon + "<br/>" +
                            "<b>Konu : </b>" + konuTip.KonuTipAdi + "<br/>" +
                            "<b>Sınıf : </b>" + model.Sinif + "<br/>" +
                            "<b>Mesaj </b>: " + model.Mesaj;

            return await EpostaGonder(icerik, subeId);
        }

        public async Task<bool> IletisimTalepEpostaGonder(IletisimTalepViewModel model, int subeId)
        {
            string icerik = "<h4>İletişim Talep</h4><br/>" +
                            "<b>Ad Soyad : </b>" + model.AdSoyad + "<br/>" +
                            "<b>Konu : </b>" + model.Konu + "<br/>" +
                            "<b>E-posta </b>: " + model.Eposta + "<br/>" +
                            "<b>Telefon </b>: " + model.Telefon + "<br/>" +
                            "<b>Mesaj </b>: " + model.Mesaj;

            return await EpostaGonder(icerik, subeId);
        }

        public async Task<bool> FranchiseTalepEpostaGonder(FranchiseTalepViewModel model, int subeId)
        {
            SubeSehirBS _SubeSehirBS = new SubeSehirBS();
            var sehir = await _SubeSehirBS.SehirBilgiDataGetir(model.SehirId);

            KurumTipBS _KurumTipBS = new KurumTipBS();
            var kurumTip = await _KurumTipBS.KurumTipDataGetir(model.KurumTipId);

            string icerik = "<h4>Franchise Talep</h4><br/>" +
                            "<b>Ad : </b>" + model.Ad + "<br/>" +
                            "<b>Soyad : </b>" + model.Soyad + "<br/>" +
                            "<b>Telefon : </b>" + model.Telefon + "<br/>" +
                            "<b>E-posta </b>: " + model.Eposta + "<br/>" +
                            "<b>Şehir </b>: " + sehir.SehirAdi + "<br/>" +
                            "<b>Kurum Tip </b>: " + kurumTip.KurumTipAdi + "<br/>" +
                            "<b>Açıklama </b>: " + model.Aciklama;

            return await EpostaGonder(icerik, subeId);
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;
using PsychosocialSupportPlatformAPI.Entity.Entities.Messages;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using System.Net;
using System.Net.Mail;
using System.Numerics;
using System.Web;

namespace PsychosocialSupportPlatformAPI.Business.Mails
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<Doctor> _doctorManager;
        private readonly UserManager<Patient> _patientManager;

        public MailService(
            IConfiguration configuration,
            UserManager<Doctor> doctorManager,
            UserManager<Patient> patientManager)
        {
            _configuration = configuration;
            _doctorManager = doctorManager;
            _patientManager = patientManager;
        }

        public async Task SendEmailToDoctorForCancelAppointment(AppointmentSchedule appointment, CancellationToken cancellationToken)
        {
            var mail = new MailMessage()
            {
                From = new MailAddress(_configuration["Mailing:Sender"]!),
                Subject = "Randevunuz İptal Edildi",
                IsBodyHtml = true,
                Body = $@"
<!DOCTYPE html
  PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
<html xmlns='http://www.w3.org/1999/xhtml' style='background-color: rgb(240, 240, 240);'>

<head>
  <title></title>
  <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
  <meta charset='utf-8' />
  <meta name='viewport' content='width=device-width, initial-scale=1' />
  <meta http-equiv='X-UA-Compatible' content='IE=edge' />

</head>
<body
  style='margin: 0 !important; padding: 0 !important;   height: 100%; margin: 0; padding: 0; width: 100%; font-family: GT America Regular, Roboto , Helvetica, Arial , sans-serif; font-weight: 400; color: rgb(79, 79, 101); -webkit-font-smoothing: antialiased;  -moz-osx-font-smoothing: grayscale; font-smoothing: always; text-rendering: optimizeLegibility;'>
  <!-- HIDDEN PREHEADER TEXT -->
  <div class='preheader'
    style='display: none; font-size: 1px; color: rgb(255, 255, 255); line-height: 1px; max-height: 0px; max-width: 0px; opacity: 0; overflow: hidden;'>
  </div>
  <table border='0' cellpadding='0' cellspacing='0' width='100%' class='mainTable  '
    style='border-collapse: collapse; background-color: rgb(240, 240, 240);'>
    <!-- HEADER -->
    <tr>
      <td align='center' class='header'>
        <table border='0' cellpadding='0' cellspacing='0' width='100%' class='Content'
          style='   border-collapse: collapse; width: 580px; margin: 0 auto;'>
          <tr class='spacer'>
            <td height='12px' colspan='2'
              style='font-size: 12px; line-height:12px;  '>
              &nbsp;</td>
          </tr>
        </table>
      </td>
    </tr>
    <!-- CONTENT -->
    <tr>
      <td align='center'>
        <table border='0' cellpadding='0' cellspacing='0' width='100%' class='Content bg-white'
          style=' border-collapse: collapse; background-color: white; width: 580px; margin: 0 auto;'>
          <tr>
            <td class='Content-container Content-container--main text textColorNormal'
              style='font-family:  GT America Regular ,  Roboto ,  Helvetica ,  Arial , sans-serif; font-weight: 400; font-size: 16px; line-height: 21px; color: rgb(79, 79, 101); padding-left: 60px; padding-right: 60px; padding-top: 54px; padding-bottom: 60px;'>
              <table width='100%' border='0' cellspacing='0' cellpadding='0'
                style='border-collapse: collapse;'>
                <tr>
                  <td valign='top' align='left'>
                    <table width='100%' border='0' cellspacing='0' cellpadding='0'
                      style='border-collapse: collapse;'>
                      <tr>
                        <td align='left'>
                          <span class='h1 textColorDark'
                            style='font-family:  GT America Condensed Bold ,  Roboto Condensed ,  Roboto , Helvetica ,  Arial , sans-serif; font-weight: 700; vertical-align: middle; font-size: 36px; line-height: 42px; color: rgb(35, 35, 62);'>Randevunuz İptal Edildi</span> </td>
                      </tr>
                      <tr>
                        <td align='left' colspan='2' valign='top' width='100%' height='1' class='hr'
                          style='background-color: rgb(211, 211, 216); border-collapse: collapse; line-height: 1px;'>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td valign='top' align='left'>
                    <table border='0' cellpadding='0' cellspacing='0' width='100%'
                      style='border-collapse: collapse;'>
                      <tr>
                        <td align='left' class='text textColorNormal'
                          style='  font-family:  GT America Regular , Roboto , Helvetica ,  Arial , sans-serif; font-weight: 400; font-size: 16px; line-height: 21px; color: rgb(79, 79, 101);'>
                          <h4>Sayın {appointment.Doctor.DoctorTitle.Title.ToUpper()} {appointment.Doctor.Name.ToUpper()} {appointment.Doctor.Surname.ToUpper()}</h4>                                
                          <p>{appointment.Day.ToShortDateString()} {(int)appointment.TimeRange}.00 Tarihli Randevunuz {appointment.Patient!.Name.ToUpper()} {appointment.Patient.Surname.ToUpper()} Tarafından İptal Edilmiştir.</p>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
        </table>
      </td>
    </tr>
    <!-- FOOTER -->
    <tr>
      <td align='center' class='Content'
        style='  width: 580px; margin: 0 auto;'>
        <table width='100%' border='0' cellspacing='0' cellpadding='0' align='center' class='Content-container'
          style='border-collapse: collapse; padding-left: 60px; padding-right: 60px;'>
          <tr class='spacer'>
            <td height='12px' colspan='2'
              style='font-size: 12px; line-height:12px;  '>
              &nbsp;</td>
          </tr>
          <tr>
            <td align='center'>
              <div class='text-xsmall textColorNormal'
                style='font-family:  GT America Regular,Roboto,Helvetica,Arial , sans-serif; font-size: 11px;  line-height: 22px; letter-spacing: 1px; color: rgb(79, 79, 101);'>
                © 2024 ARTI BİR DESTEK
              </div>
            </td>
          </tr>
          <tr class='spacer'>
            <td height='12px' colspan='2'
              style='font-size: 12px; line-height:12px;  '>
              &nbsp;</td>
          </tr>
        </table>
      </td>
    </tr>
  </table>
</body>
</html>"
            };

            mail.To.Add(new MailAddress(appointment.Doctor!.Email));

            using SmtpClient client = new();
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_configuration["Mailing:Sender"], _configuration["Mailing:Password"]);
            client.Host = _configuration["Mailing:Host"]!;
            client.Port = Convert.ToInt32(_configuration["Mailing:Port"]);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            await client.SendMailAsync(mail, cancellationToken);
        }

        public async Task SendEmailToPatientForCancelAppointment(AppointmentSchedule appointment, CancellationToken cancellationToken)
        {
            var mail = new MailMessage()
            {
                From = new MailAddress(_configuration["Mailing:Sender"]!),
                Subject = "Randevunuz İptal Edildi",
                IsBodyHtml = true,
                Body = $@"
<!DOCTYPE html
  PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
<html xmlns='http://www.w3.org/1999/xhtml' style='background-color: rgb(240, 240, 240);'>

<head>
  <title></title>
  <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
  <meta charset='utf-8' />
  <meta name='viewport' content='width=device-width, initial-scale=1' />
  <meta http-equiv='X-UA-Compatible' content='IE=edge' />

</head>
<body
  style='margin: 0 !important; padding: 0 !important;   height: 100%; margin: 0; padding: 0; width: 100%; font-family: GT America Regular, Roboto , Helvetica, Arial , sans-serif; font-weight: 400; color: rgb(79, 79, 101); -webkit-font-smoothing: antialiased;  -moz-osx-font-smoothing: grayscale; font-smoothing: always; text-rendering: optimizeLegibility;'>
  <!-- HIDDEN PREHEADER TEXT -->
  <div class='preheader'
    style='display: none; font-size: 1px; color: rgb(255, 255, 255); line-height: 1px; max-height: 0px; max-width: 0px; opacity: 0; overflow: hidden;'>
  </div>
  <table border='0' cellpadding='0' cellspacing='0' width='100%' class='mainTable  '
    style='border-collapse: collapse; background-color: rgb(240, 240, 240);'>
    <!-- HEADER -->
    <tr>
      <td align='center' class='header'>
        <table border='0' cellpadding='0' cellspacing='0' width='100%' class='Content'
          style='   border-collapse: collapse; width: 580px; margin: 0 auto;'>
          <tr class='spacer'>
            <td height='12px' colspan='2'
              style='font-size: 12px; line-height:12px;  '>
              &nbsp;</td>
          </tr>
        </table>
      </td>
    </tr>
    <!-- CONTENT -->
    <tr>
      <td align='center'>
        <table border='0' cellpadding='0' cellspacing='0' width='100%' class='Content bg-white'
          style=' border-collapse: collapse; background-color: white; width: 580px; margin: 0 auto;'>
          <tr>
            <td class='Content-container Content-container--main text textColorNormal'
              style='font-family:  GT America Regular ,  Roboto ,  Helvetica ,  Arial , sans-serif; font-weight: 400; font-size: 16px; line-height: 21px; color: rgb(79, 79, 101); padding-left: 60px; padding-right: 60px; padding-top: 54px; padding-bottom: 60px;'>
              <table width='100%' border='0' cellspacing='0' cellpadding='0'
                style='border-collapse: collapse;'>
                <tr>
                  <td valign='top' align='left'>
                    <table width='100%' border='0' cellspacing='0' cellpadding='0'
                      style='border-collapse: collapse;'>
                      <tr>
                        <td align='left'>
                          <span class='h1 textColorDark'
                            style='font-family:  GT America Condensed Bold ,  Roboto Condensed ,  Roboto , Helvetica ,  Arial , sans-serif; font-weight: 700; vertical-align: middle; font-size: 36px; line-height: 42px; color: rgb(35, 35, 62);'>Randevunuz İptal Edildi</span> </td>
                      </tr>
                      <tr>
                        <td align='left' colspan='2' valign='top' width='100%' height='1' class='hr'
                          style='background-color: rgb(211, 211, 216); border-collapse: collapse; line-height: 1px;'>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td valign='top' align='left'>
                    <table border='0' cellpadding='0' cellspacing='0' width='100%'
                      style='border-collapse: collapse;'>
                      <tr>
                        <td align='left' class='text textColorNormal'
                          style='  font-family:  GT America Regular , Roboto , Helvetica ,  Arial , sans-serif; font-weight: 400; font-size: 16px; line-height: 21px; color: rgb(79, 79, 101);'>
                          <h4>Sayın {appointment.Patient!.Name.ToUpper()} {appointment.Patient.Surname.ToUpper()}</h4>                                
                          <p>{appointment.Day.ToShortDateString()} {(int)appointment.TimeRange}.00 Tarihli Randevunuz {appointment.Doctor.DoctorTitle.Title.ToUpper()} {appointment.Doctor.Name.ToUpper()} {appointment.Doctor.Surname.ToUpper()} Tarafından İptal Edilmiştir.</p>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
        </table>
      </td>
    </tr>
    <!-- FOOTER -->
    <tr>
      <td align='center' class='Content'
        style='  width: 580px; margin: 0 auto;'>
        <table width='100%' border='0' cellspacing='0' cellpadding='0' align='center' class='Content-container'
          style='border-collapse: collapse; padding-left: 60px; padding-right: 60px;'>
          <tr class='spacer'>
            <td height='12px' colspan='2'
              style='font-size: 12px; line-height:12px;  '>
              &nbsp;</td>
          </tr>
          <tr>
            <td align='center'>
              <div class='text-xsmall textColorNormal'
                style='font-family:  GT America Regular,Roboto,Helvetica,Arial , sans-serif; font-size: 11px;  line-height: 22px; letter-spacing: 1px; color: rgb(79, 79, 101);'>
                © 2024 ARTI BİR DESTEK
              </div>
            </td>
          </tr>
          <tr class='spacer'>
            <td height='12px' colspan='2'
              style='font-size: 12px; line-height:12px;  '>
              &nbsp;</td>
          </tr>
        </table>
      </td>
    </tr>
  </table>
</body>
</html>"
            };

            mail.To.Add(new MailAddress(appointment.Patient!.Email));

            using SmtpClient client = new();
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_configuration["Mailing:Sender"], _configuration["Mailing:Password"]);
            client.Host = _configuration["Mailing:Host"]!;
            client.Port = Convert.ToInt32(_configuration["Mailing:Port"]);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            await client.SendMailAsync(mail, cancellationToken);
        }

        public async Task SendEmailToDoctorForConfirmationAccount(Doctor doctor, CancellationToken cancellationToken)
        {
            var mail = new MailMessage()
            {
                From = new MailAddress(_configuration["Mailing:Sender"]!),
                Subject = "Hesabınız Onaylandı",
                IsBodyHtml = true,
                Body = $@"
<!DOCTYPE html
  PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
<html xmlns='http://www.w3.org/1999/xhtml' style='background-color: rgb(240, 240, 240);'>

<head>
  <title></title>
  <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
  <meta charset='utf-8' />
  <meta name='viewport' content='width=device-width, initial-scale=1' />
  <meta http-equiv='X-UA-Compatible' content='IE=edge' />

</head>
<body
  style='margin: 0 !important; padding: 0 !important;   height: 100%; margin: 0; padding: 0; width: 100%; font-family: GT America Regular, Roboto , Helvetica, Arial , sans-serif; font-weight: 400; color: rgb(79, 79, 101); -webkit-font-smoothing: antialiased;  -moz-osx-font-smoothing: grayscale; font-smoothing: always; text-rendering: optimizeLegibility;'>
  <!-- HIDDEN PREHEADER TEXT -->
  <div class='preheader'
    style='display: none; font-size: 1px; color: rgb(255, 255, 255); line-height: 1px; max-height: 0px; max-width: 0px; opacity: 0; overflow: hidden;'>
  </div>
  <table border='0' cellpadding='0' cellspacing='0' width='100%' class='mainTable  '
    style='border-collapse: collapse; background-color: rgb(240, 240, 240);'>
    <!-- HEADER -->
    <tr>
      <td align='center' class='header'>
        <table border='0' cellpadding='0' cellspacing='0' width='100%' class='Content'
          style='   border-collapse: collapse; width: 580px; margin: 0 auto;'>
          <tr class='spacer'>
            <td height='12px' colspan='2'
              style='font-size: 12px; line-height:12px;  '>
              &nbsp;</td>
          </tr>
        </table>
      </td>
    </tr>
    <!-- CONTENT -->
    <tr>
      <td align='center'>
        <table border='0' cellpadding='0' cellspacing='0' width='100%' class='Content bg-white'
          style=' border-collapse: collapse; background-color: white; width: 580px; margin: 0 auto;'>
          <tr>
            <td class='Content-container Content-container--main text textColorNormal'
              style='font-family:  GT America Regular ,  Roboto ,  Helvetica ,  Arial , sans-serif; font-weight: 400; font-size: 16px; line-height: 21px; color: rgb(79, 79, 101); padding-left: 60px; padding-right: 60px; padding-top: 54px; padding-bottom: 60px;'>
              <table width='100%' border='0' cellspacing='0' cellpadding='0'
                style='border-collapse: collapse;'>
                <tr>
                  <td valign='top' align='left'>
                    <table width='100%' border='0' cellspacing='0' cellpadding='0'
                      style='border-collapse: collapse;'>
                      <tr>
                        <td align='left'>
                          <span class='h1 textColorDark'
                            style='font-family:  GT America Condensed Bold ,  Roboto Condensed ,  Roboto , Helvetica ,  Arial , sans-serif; font-weight: 700; vertical-align: middle; font-size: 36px; line-height: 42px; color: rgb(35, 35, 62);'>Hesabınız Onaylandı</span> </td>
                      </tr>
                      <tr>
                        <td align='left' colspan='2' valign='top' width='100%' height='1' class='hr'
                          style='background-color: rgb(211, 211, 216); border-collapse: collapse; line-height: 1px;'>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td valign='top' align='left'>
                    <table border='0' cellpadding='0' cellspacing='0' width='100%'
                      style='border-collapse: collapse;'>
                      <tr>
                        <td align='left' class='text textColorNormal'
                          style='  font-family:  GT America Regular , Roboto , Helvetica ,  Arial , sans-serif; font-weight: 400; font-size: 16px; line-height: 21px; color: rgb(79, 79, 101);'>
                          <p>Sayın {doctor.DoctorTitle.Title.ToUpper()} {doctor.Name.ToUpper()} {doctor.Surname.ToUpper()} Hesabınız Onaylanmıştır. Sisteme Erişim Sağlayabilirsiniz.</p>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
        </table>
      </td>
    </tr>
    <!-- FOOTER -->
    <tr>
      <td align='center' class='Content'
        style='  width: 580px; margin: 0 auto;'>
        <table width='100%' border='0' cellspacing='0' cellpadding='0' align='center' class='Content-container'
          style='border-collapse: collapse; padding-left: 60px; padding-right: 60px;'>
          <tr class='spacer'>
            <td height='12px' colspan='2'
              style='font-size: 12px; line-height:12px;  '>
              &nbsp;</td>
          </tr>
          <tr>
            <td align='center'>
              <div class='text-xsmall textColorNormal'
                style='font-family:  GT America Regular,Roboto,Helvetica,Arial , sans-serif; font-size: 11px;  line-height: 22px; letter-spacing: 1px; color: rgb(79, 79, 101);'>
                © 2024 ARTI BİR DESTEK
              </div>
            </td>
          </tr>
          <tr class='spacer'>
            <td height='12px' colspan='2'
              style='font-size: 12px; line-height:12px;  '>
              &nbsp;</td>
          </tr>
        </table>
      </td>
    </tr>
  </table>
</body>
</html>"
            };

            mail.To.Add(new MailAddress(doctor.Email));

            using SmtpClient client = new();
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_configuration["Mailing:Sender"], _configuration["Mailing:Password"]);
            client.Host = _configuration["Mailing:Host"]!;
            client.Port = Convert.ToInt32(_configuration["Mailing:Port"]);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            await client.SendMailAsync(mail, cancellationToken);
        }

        public async Task SendEmailForForgotPassword(ApplicationUser user, string token, CancellationToken cancellationToken)
        {
            var mail = new MailMessage()
            {
                From = new MailAddress(_configuration["Mailing:Sender"]!),
                Subject = "Şifremi Unuttum",
                IsBodyHtml = true,
                Body = $@"
<!DOCTYPE html
  PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
<html xmlns='http://www.w3.org/1999/xhtml' style='background-color: rgb(240, 240, 240);'>

<head>
  <title></title>
  <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
  <meta charset='utf-8' />
  <meta name='viewport' content='width=device-width, initial-scale=1' />
  <meta http-equiv='X-UA-Compatible' content='IE=edge' />

</head>
<body
  style='margin: 0 !important; padding: 0 !important;   height: 100%; margin: 0; padding: 0; width: 100%; font-family: GT America Regular, Roboto , Helvetica, Arial , sans-serif; font-weight: 400; color: rgb(79, 79, 101); -webkit-font-smoothing: antialiased;  -moz-osx-font-smoothing: grayscale; font-smoothing: always; text-rendering: optimizeLegibility;'>
  <!-- HIDDEN PREHEADER TEXT -->
  <div class='preheader'
    style='display: none; font-size: 1px; color: rgb(255, 255, 255); line-height: 1px; max-height: 0px; max-width: 0px; opacity: 0; overflow: hidden;'>
  </div>
  <table border='0' cellpadding='0' cellspacing='0' width='100%' class='mainTable  '
    style='border-collapse: collapse; background-color: rgb(240, 240, 240);'>
    <!-- HEADER -->
    <tr>
      <td align='center' class='header'>
        <table border='0' cellpadding='0' cellspacing='0' width='100%' class='Content'
          style='   border-collapse: collapse; width: 580px; margin: 0 auto;'>
          <tr class='spacer'>
            <td height='12px' colspan='2'
              style='font-size: 12px; line-height:12px;  '>
              &nbsp;</td>
          </tr>
        </table>
      </td>
    </tr>
    <!-- CONTENT -->
    <tr>
      <td align='center'>
        <table border='0' cellpadding='0' cellspacing='0' width='100%' class='Content bg-white'
          style=' border-collapse: collapse; background-color: white; width: 580px; margin: 0 auto;'>
          <tr>
            <td class='Content-container Content-container--main text textColorNormal'
              style='font-family:  GT America Regular ,  Roboto ,  Helvetica ,  Arial , sans-serif; font-weight: 400; font-size: 16px; line-height: 21px; color: rgb(79, 79, 101); padding-left: 60px; padding-right: 60px; padding-top: 54px; padding-bottom: 60px;'>
              <table width='100%' border='0' cellspacing='0' cellpadding='0'
                style='border-collapse: collapse;'>
                <tr>
                  <td valign='top' align='left'>
                    <table width='100%' border='0' cellspacing='0' cellpadding='0'
                      style='border-collapse: collapse;'>
                      <tr>
                        <td align='left'>
                          <span class='h1 textColorDark'
                            style='font-family:  GT America Condensed Bold ,  Roboto Condensed ,  Roboto , Helvetica ,  Arial , sans-serif; font-weight: 700; vertical-align: middle; font-size: 36px; line-height: 42px; color: rgb(35, 35, 62);'>Şifre
                            Sıfırla</span> </td>
                      </tr>
                      <tr>
                        <td align='left' colspan='2' valign='top' width='100%' height='1' class='hr'
                          style='background-color: rgb(211, 211, 216); border-collapse: collapse; line-height: 1px;'>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td valign='top' align='left'>
                    <table border='0' cellpadding='0' cellspacing='0' width='100%'
                      style='border-collapse: collapse;'>
                      <tr>
                        <td align='left' class='text textColorNormal'
                          style='  font-family:  GT America Regular , Roboto , Helvetica ,  Arial , sans-serif; font-weight: 400; font-size: 16px; line-height: 21px; color: rgb(79, 79, 101);'>
                          <h4>Sayın {user.Name.ToUpper()} {user.Surname.ToUpper()}</h4>                                
                          <p>Şifrenizi Güncellemek İçin Butona Tıklayın:</p>
                        </td>
                      </tr>
                      <tr>
                        <td align='center' valign='center' width='100%'>
                          <table border='0' cellspacing='0' cellpadding='0' width='100%'
                            style='   border-collapse: collapse;'>
                            <tr>
                              <td align='center' valign='center' width='100%' class='Button-primary-wrapper'
                                style='  border-radius: 3px; background-color: #db2777;'>
                                <a href='{_configuration["Urls:FrontUrl"]}/reset-password?token={HttpUtility.UrlEncode(token)}&mail={user.Email}'' target='
                                  _blank' class='Button-primary'
                                  style='  font-family:  GT America Medium ,  Roboto ,  Helvetica ,  Arial , sans-serif; border-radius: 3px; border: 1px solid #db2777; color: rgb(255, 255, 255); display: block; font-size: 16px; font-weight: 600; padding: 18px; text-decoration: none;'>
                                  Şifrenizi sıfırlayın </a> </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
        </table>
      </td>
    </tr>
    <!-- FOOTER -->
    <tr>
      <td align='center' class='Content'
        style='  width: 580px; margin: 0 auto;'>
        <table width='100%' border='0' cellspacing='0' cellpadding='0' align='center' class='Content-container'
          style='border-collapse: collapse; padding-left: 60px; padding-right: 60px;'>
          <tr class='spacer'>
            <td height='12px' colspan='2'
              style='font-size: 12px; line-height:12px;  '>
              &nbsp;</td>
          </tr>
          <tr>
            <td align='center'>
              <div class='text-xsmall textColorNormal'
                style='font-family:  GT America Regular,Roboto,Helvetica,Arial , sans-serif; font-size: 11px;  line-height: 22px; letter-spacing: 1px; color: rgb(79, 79, 101);'>
                © 2024 ARTI BİR DESTEK
              </div>
            </td>
          </tr>
          <tr class='spacer'>
            <td height='12px' colspan='2'
              style='font-size: 12px; line-height:12px;  '>
              &nbsp;</td>
          </tr>
        </table>
      </td>
    </tr>
  </table>
</body>
</html>"
            };
            mail.To.Add(new MailAddress(user.Email));

            using SmtpClient client = new();
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_configuration["Mailing:Sender"], _configuration["Mailing:Password"]);
            client.Host = _configuration["Mailing:Host"]!;
            client.Port = Convert.ToInt32(_configuration["Mailing:Port"]);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            await client.SendMailAsync(mail, cancellationToken);
        }

        public async Task SendEmailForConfirmEmail(string email, string token, CancellationToken cancellationToken)
        {
            var mail = new MailMessage()
            {
                From = new MailAddress(_configuration["Mailing:Sender"]!),
                Subject = "Hesabınızı Doğrulayın",
                IsBodyHtml = true,
                Body = $@"
<!DOCTYPE html
  PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
<html xmlns='http://www.w3.org/1999/xhtml' style='background-color: rgb(240, 240, 240);'>

<head>
  <title></title>
  <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
  <meta charset='utf-8' />
  <meta name='viewport' content='width=device-width, initial-scale=1' />
  <meta http-equiv='X-UA-Compatible' content='IE=edge' />

</head>
<body
  style='margin: 0 !important; padding: 0 !important;   height: 100%; margin: 0; padding: 0; width: 100%; font-family: GT America Regular, Roboto , Helvetica, Arial , sans-serif; font-weight: 400; color: rgb(79, 79, 101); -webkit-font-smoothing: antialiased;  -moz-osx-font-smoothing: grayscale; font-smoothing: always; text-rendering: optimizeLegibility;'>
  <!-- HIDDEN PREHEADER TEXT -->
  <div class='preheader'
    style='display: none; font-size: 1px; color: rgb(255, 255, 255); line-height: 1px; max-height: 0px; max-width: 0px; opacity: 0; overflow: hidden;'>
  </div>
  <table border='0' cellpadding='0' cellspacing='0' width='100%' class='mainTable  '
    style='border-collapse: collapse; background-color: rgb(240, 240, 240);'>
    <!-- HEADER -->
    <tr>
      <td align='center' class='header'>
        <table border='0' cellpadding='0' cellspacing='0' width='100%' class='Content'
          style='   border-collapse: collapse; width: 580px; margin: 0 auto;'>
          <tr class='spacer'>
            <td height='12px' colspan='2'
              style='font-size: 12px; line-height:12px;  '>
              &nbsp;</td>
          </tr>
        </table>
      </td>
    </tr>
    <!-- CONTENT -->
    <tr>
      <td align='center'>
        <table border='0' cellpadding='0' cellspacing='0' width='100%' class='Content bg-white'
          style=' border-collapse: collapse; background-color: white; width: 580px; margin: 0 auto;'>
          <tr>
            <td class='Content-container Content-container--main text textColorNormal'
              style='font-family:  GT America Regular ,  Roboto ,  Helvetica ,  Arial , sans-serif; font-weight: 400; font-size: 16px; line-height: 21px; color: rgb(79, 79, 101); padding-left: 60px; padding-right: 60px; padding-top: 54px; padding-bottom: 60px;'>
              <table width='100%' border='0' cellspacing='0' cellpadding='0'
                style='border-collapse: collapse;'>
                <tr>
                  <td valign='top' align='left'>
                    <table width='100%' border='0' cellspacing='0' cellpadding='0'
                      style='border-collapse: collapse;'>
                      <tr>
                        <td align='left'>
                          <span class='h1 textColorDark'
                            style='font-family:  GT America Condensed Bold ,  Roboto Condensed ,  Roboto , Helvetica ,  Arial , sans-serif; font-weight: 700; vertical-align: middle; font-size: 36px; line-height: 42px; color: rgb(35, 35, 62);'>Hesabınızı Doğrulayın</span> </td>
                      </tr>
                      <tr>
                        <td align='left' colspan='2' valign='top' width='100%' height='1' class='hr'
                          style='background-color: rgb(211, 211, 216); border-collapse: collapse; line-height: 1px;'>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td valign='top' align='left'>
                    <table border='0' cellpadding='0' cellspacing='0' width='100%'
                      style='border-collapse: collapse;'>
                      <tr>
                        <td align='left' class='text textColorNormal'
                          style='  font-family:  GT America Regular , Roboto , Helvetica ,  Arial , sans-serif; font-weight: 400; font-size: 16px; line-height: 21px; color: rgb(79, 79, 101);'>
                          <p>Hesabınızı Onaylamak İçin Butona Tıklayın:</p>
                        </td>
                      </tr>
                      <tr>
                        <td align='center' valign='center' width='100%'>
                          <table border='0' cellspacing='0' cellpadding='0' width='100%'
                            style='   border-collapse: collapse;'>
                            <tr>
                              <td align='center' valign='center' width='100%' class='Button-primary-wrapper'
                                style='  border-radius: 3px; background-color: #db2777;'>
                                <a href='{_configuration["Urls:BaseUrl"]}/api/Authentication/ConfirmEmail?email={HttpUtility.UrlEncode(email)}&token={HttpUtility.UrlEncode(token)}'' target='
                                  _blank' class='Button-primary'
                                  style='  font-family:  GT America Medium ,  Roboto ,  Helvetica ,  Arial , sans-serif; border-radius: 3px; border: 1px solid #db2777; color: rgb(255, 255, 255); display: block; font-size: 16px; font-weight: 600; padding: 18px; text-decoration: none;'>
                                  Hesabınızı Doğrulayın </a> </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
        </table>
      </td>
    </tr>
    <!-- FOOTER -->
    <tr>
      <td align='center' class='Content'
        style='  width: 580px; margin: 0 auto;'>
        <table width='100%' border='0' cellspacing='0' cellpadding='0' align='center' class='Content-container'
          style='border-collapse: collapse; padding-left: 60px; padding-right: 60px;'>
          <tr class='spacer'>
            <td height='12px' colspan='2'
              style='font-size: 12px; line-height:12px;  '>
              &nbsp;</td>
          </tr>
          <tr>
            <td align='center'>
              <div class='text-xsmall textColorNormal'
                style='font-family:  GT America Regular,Roboto,Helvetica,Arial , sans-serif; font-size: 11px;  line-height: 22px; letter-spacing: 1px; color: rgb(79, 79, 101);'>
                © 2024 ARTI BİR DESTEK
              </div>
            </td>
          </tr>
          <tr class='spacer'>
            <td height='12px' colspan='2'
              style='font-size: 12px; line-height:12px;  '>
              &nbsp;</td>
          </tr>
        </table>
      </td>
    </tr>
  </table>
</body>
</html>"
            };
            mail.To.Add(new MailAddress(email));

            using SmtpClient client = new();
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_configuration["Mailing:Sender"], _configuration["Mailing:Password"]);
            client.Host = _configuration["Mailing:Host"]!;
            client.Port = Convert.ToInt32(_configuration["Mailing:Port"]);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            await client.SendMailAsync(mail, cancellationToken);
        }

        public async Task SendEmailToDoctorForEmergency(string doctorId, string patientId, string message)
        {
            Patient patient = await _patientManager.Users.AsNoTracking().Where(p => p.Id == patientId).FirstAsync();
            Doctor doctor = await _doctorManager.Users.AsNoTracking().Where(d => d.Id == doctorId).FirstAsync();

            var mail = new MailMessage()
            {
                From = new MailAddress(_configuration["Mailing:Sender"]!),
                Subject = "Acil Durum Bildirimi",
                IsBodyHtml = true,
                Body = $@"
<!DOCTYPE html
  PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
<html xmlns='http://www.w3.org/1999/xhtml' style='background-color: rgb(240, 240, 240);'>

<head>
  <title></title>
  <meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />
  <meta charset='utf-8' />
  <meta name='viewport' content='width=device-width, initial-scale=1' />
  <meta http-equiv='X-UA-Compatible' content='IE=edge' />

</head>
<body
  style='margin: 0 !important; padding: 0 !important;   height: 100%; margin: 0; padding: 0; width: 100%; font-family: GT America Regular, Roboto , Helvetica, Arial , sans-serif; font-weight: 400; color: rgb(79, 79, 101); -webkit-font-smoothing: antialiased;  -moz-osx-font-smoothing: grayscale; font-smoothing: always; text-rendering: optimizeLegibility;'>
  <!-- HIDDEN PREHEADER TEXT -->
  <div class='preheader'
    style='display: none; font-size: 1px; color: rgb(255, 255, 255); line-height: 1px; max-height: 0px; max-width: 0px; opacity: 0; overflow: hidden;'>
  </div>
  <table border='0' cellpadding='0' cellspacing='0' width='100%' class='mainTable  '
    style='border-collapse: collapse; background-color: rgb(240, 240, 240);'>
    <!-- HEADER -->
    <tr>
      <td align='center' class='header'>
        <table border='0' cellpadding='0' cellspacing='0' width='100%' class='Content'
          style='   border-collapse: collapse; width: 580px; margin: 0 auto;'>
          <tr class='spacer'>
            <td height='12px' colspan='2'
              style='font-size: 12px; line-height:12px;  '>
              &nbsp;</td>
          </tr>
        </table>
      </td>
    </tr>
    <!-- CONTENT -->
    <tr>
      <td align='center'>
        <table border='0' cellpadding='0' cellspacing='0' width='100%' class='Content bg-white'
          style=' border-collapse: collapse; background-color: white; width: 580px; margin: 0 auto;'>
          <tr>
            <td class='Content-container Content-container--main text textColorNormal'
              style='font-family:  GT America Regular ,  Roboto ,  Helvetica ,  Arial , sans-serif; font-weight: 400; font-size: 16px; line-height: 21px; color: rgb(79, 79, 101); padding-left: 60px; padding-right: 60px; padding-top: 54px; padding-bottom: 60px;'>
              <table width='100%' border='0' cellspacing='0' cellpadding='0'
                style='border-collapse: collapse;'>
                <tr>
                  <td valign='top' align='left'>
                    <table width='100%' border='0' cellspacing='0' cellpadding='0'
                      style='border-collapse: collapse;'>
                      <tr>
                        <td align='left'>
                          <span class='h1 textColorDark'
                            style='font-family:  GT America Condensed Bold ,  Roboto Condensed ,  Roboto , Helvetica ,  Arial , sans-serif; font-weight: 700; vertical-align: middle; font-size: 36px; line-height: 42px; color: rgb(35, 35, 62);'>!!! Acil Durum !!!</span> </td>
                      </tr>
                      <tr>
                        <td align='left' colspan='2' valign='top' width='100%' height='1' class='hr'
                          style='background-color: rgb(211, 211, 216); border-collapse: collapse; line-height: 1px;'>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
                <tr>
                  <td valign='top' align='left'>
                    <table border='0' cellpadding='0' cellspacing='0' width='100%'
                      style='border-collapse: collapse;'>
                      <tr>
                        <td align='left' class='text textColorNormal'
                          style='  font-family:  GT America Regular , Roboto , Helvetica ,  Arial , sans-serif; font-weight: 400; font-size: 16px; line-height: 21px; color: rgb(79, 79, 101);'>
                          <h4>Sayın {doctor.DoctorTitle.Title.ToUpper()} {doctor.Name.ToUpper()} {doctor.Surname.ToUpper()} Danışanınız {patient.Name.ToUpper()} {patient.Surname.ToUpper()} Acil Durum İçeren Mesaj Gönderdi.</h4> <p>Gönderilen Mesaj: {message}</p>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
        </table>
      </td>
    </tr>
    <!-- FOOTER -->
    <tr>
      <td align='center' class='Content'
        style='  width: 580px; margin: 0 auto;'>
        <table width='100%' border='0' cellspacing='0' cellpadding='0' align='center' class='Content-container'
          style='border-collapse: collapse; padding-left: 60px; padding-right: 60px;'>
          <tr class='spacer'>
            <td height='12px' colspan='2'
              style='font-size: 12px; line-height:12px;  '>
              &nbsp;</td>
          </tr>
          <tr>
            <td align='center'>
              <div class='text-xsmall textColorNormal'
                style='font-family:  GT America Regular,Roboto,Helvetica,Arial , sans-serif; font-size: 11px;  line-height: 22px; letter-spacing: 1px; color: rgb(79, 79, 101);'>
                © 2024 ARTI BİR DESTEK
              </div>
            </td>
          </tr>
          <tr class='spacer'>
            <td height='12px' colspan='2'
              style='font-size: 12px; line-height:12px;  '>
              &nbsp;</td>
          </tr>
        </table>
      </td>
    </tr>
  </table>
</body>
</html>"
            };
            mail.To.Add(new MailAddress(doctor.Email));

            using SmtpClient client = new();
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_configuration["Mailing:Sender"], _configuration["Mailing:Password"]);
            client.Host = _configuration["Mailing:Host"]!;
            client.Port = Convert.ToInt32(_configuration["Mailing:Port"]);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            await client.SendMailAsync(mail);
        }
    }
}

//function _() {
//    var UserName = $('#UserName').val();
//    var Password = $('#Password').val();

//    //if ($("#UserName").val() == "") {
//    //    alert('Ingrese usuario');
//    //    return false;
//    //}
//    //if ($("#Password").val() == "") {
//    //    alert('Ingrese password');
//    //    return false;
//    //}
//    //else {
//        var key = CryptoJS.enc.Utf8.parse('8080808080808080');
//        var iv = CryptoJS.enc.Utf8.parse('8080808080808080');

//        var encryptedlogin =
//            CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse("123"), key,
//                { keySize: 128 / 8, iv: iv, mode: CryptoJS.mode.CBC, padding: CryptoJS.pad.Pkcs7 });

//        $('#UserName').val(encryptedlogin);

//        var encryptedpassword =
//            CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(Password), key,
//                { keySize: 128 / 8, iv: iv, mode: CryptoJS.mode.CBC, padding: CryptoJS.pad.Pkcs7 });

//        $('#Password').val(encryptedpassword);

//    //}
//}
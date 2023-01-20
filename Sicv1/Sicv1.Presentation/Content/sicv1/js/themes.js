 var themeSwitch = document.getElementById('themeSwitch');
 if (themeSwitch) {
   themeSwitch.addEventListener('change', function (event) {
     resetTheme();
   });
   function resetTheme() {
     if (themeSwitch.checked) {
       document.body.setAttribute('data-theme', 'dark');
       localStorage.setItem('themeSwitch', 'dark');
     } else {
       document.body.setAttribute('data-theme', 'light');
       localStorage.setItem('themeSwitch', 'light');
     }
   };
 }
 //PART TWO
 var themeSwitch = document.getElementById('themeSwitch');
 if (themeSwitch) {
   initTheme();
   themeSwitch.addEventListener('change', function (event) {
     resetTheme();
   });
   function initTheme() {
     var darkThemeSelected = (localStorage.getItem('themeSwitch') !== null && localStorage.getItem('themeSwitch') === 'dark');

     themeSwitch.checked = darkThemeSelected;
     darkThemeSelected ? document.body.setAttribute('data-theme', 'dark') : document.body.setAttribute('data-theme', 'light');
   };
   function resetTheme() {
     if (themeSwitch.checked) {
       document.body.setAttribute('data-theme', 'dark');
       localStorage.setItem('themeSwitch', 'dark');
     } else {
       document.body.setAttribute('data-theme', 'light');
       localStorage.setItem('themeSwitch', 'light');
     }
   };
 }

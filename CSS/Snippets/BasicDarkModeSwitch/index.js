'use strict';

// switch modes by setting data theme attribute to 'dark'
// upon check of the particular switch element
document.getElementById('theme-switch')
    .addEventListener('change', function (event) {
        event.target.checked ?
            document.body.setAttribute('data-theme', 'dark') :
            document.body.removeAttribute('data-theme');
    });
/*! Load Universal
 */
$(document).ready(function () {
    //Init double tap for touch menu
    $('nav li:has(ul)').doubleTapToGo();
});

//Web Font Loader
WebFont.load({
    google: {
        families: ['Vollkorn:700', 'Lato:400,400i,700,700i']
    }
});
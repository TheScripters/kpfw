﻿/*!
    Double tap to open normal menu on touch
	By Osvaldas Valutis, www.osvaldas.info
	Available for use under the MIT License
*/
!function (t, n, o) { t.fn.doubleTapToGo = function () { return "ontouchstart" in n || navigator.msMaxTouchPoints || navigator.userAgent.toLowerCase().match(/windows phone os 7/i) ? (this.each(function () { var n = !1; t(this).on("click", function (o) { var i = t(this); i[0] != n[0] && (o.preventDefault(), n = i) }), t(o).on("click touchstart MSPointerDown", function (o) { for (var i = !0, a = t(o.target).parents(), e = 0; e < a.length; e++) a[e] == n[0] && (i = !1); i && (n = !1) }) }), this) : !1 } }(jQuery, window, document);
/*! modernizr 3.5.0 (Custom Build) | MIT *
 * https://modernizr.com/download/?-touchevents-setclasses !*/
!function (e, n, t) { function o(e) { var n = c.className, t = Modernizr._config.classPrefix || ""; if (p && (n = n.baseVal), Modernizr._config.enableJSClass) { var o = new RegExp("(^|\\s)" + t + "no-js(\\s|$)"); n = n.replace(o, "$1" + t + "js$2") } Modernizr._config.enableClasses && (n += " " + t + e.join(" " + t), p ? c.className.baseVal = n : c.className = n) } function s(e, n) { return typeof e === n } function a() { var e, n, t, o, a, i, r; for (var l in d) if (d.hasOwnProperty(l)) { if (e = [], n = d[l], n.name && (e.push(n.name.toLowerCase()), n.options && n.options.aliases && n.options.aliases.length)) for (t = 0; t < n.options.aliases.length; t++) e.push(n.options.aliases[t].toLowerCase()); for (o = s(n.fn, "function") ? n.fn() : n.fn, a = 0; a < e.length; a++) i = e[a], r = i.split("."), 1 === r.length ? Modernizr[r[0]] = o : (!Modernizr[r[0]] || Modernizr[r[0]] instanceof Boolean || (Modernizr[r[0]] = new Boolean(Modernizr[r[0]])), Modernizr[r[0]][r[1]] = o), f.push((o ? "" : "no-") + r.join("-")) } } function i() { return "function" != typeof n.createElement ? n.createElement(arguments[0]) : p ? n.createElementNS.call(n, "http://www.w3.org/2000/svg", arguments[0]) : n.createElement.apply(n, arguments) } function r() { var e = n.body; return e || (e = i(p ? "svg" : "body"), e.fake = !0), e } function l(e, t, o, s) { var a, l, f, d, u = "modernizr", p = i("div"), h = r(); if (parseInt(o, 10)) for (; o--;) f = i("div"), f.id = s ? s[o] : u + (o + 1), p.appendChild(f); return a = i("style"), a.type = "text/css", a.id = "s" + u, (h.fake ? h : p).appendChild(a), h.appendChild(p), a.styleSheet ? a.styleSheet.cssText = e : a.appendChild(n.createTextNode(e)), p.id = u, h.fake && (h.style.background = "", h.style.overflow = "hidden", d = c.style.overflow, c.style.overflow = "hidden", c.appendChild(h)), l = t(p, e), h.fake ? (h.parentNode.removeChild(h), c.style.overflow = d, c.offsetHeight) : p.parentNode.removeChild(p), !!l } var f = [], c = n.documentElement, d = [], u = { _version: "3.5.0", _config: { classPrefix: "", enableClasses: !0, enableJSClass: !0, usePrefixes: !0 }, _q: [], on: function (e, n) { var t = this; setTimeout(function () { n(t[e]) }, 0) }, addTest: function (e, n, t) { d.push({ name: e, fn: n, options: t }) }, addAsyncTest: function (e) { d.push({ name: null, fn: e }) } }, Modernizr = function () { }; Modernizr.prototype = u, Modernizr = new Modernizr; var p = "svg" === c.nodeName.toLowerCase(), h = u._config.usePrefixes ? " -webkit- -moz- -o- -ms- ".split(" ") : ["", ""]; u._prefixes = h; var m = u.testStyles = l; Modernizr.addTest("touchevents", function () { var t; if ("ontouchstart" in e || e.DocumentTouch && n instanceof DocumentTouch) t = !0; else { var o = ["@media (", h.join("touch-enabled),("), "heartz", ")", "{#modernizr{top:9px;position:absolute}}"].join(""); m(o, function (e) { t = 9 === e.offsetTop }) } return t }), a(), o(f), delete u.addTest, delete u.addAsyncTest; for (var v = 0; v < Modernizr._q.length; v++) Modernizr._q[v](); e.Modernizr = Modernizr }(window, document);
/*!
    Double tap to open normal menu on touch
	By Osvaldas Valutis, www.osvaldas.info
	Available for use under the MIT License
*/
!function (t, n, o) { t.fn.doubleTapToGo = function () { return "ontouchstart" in n || navigator.msMaxTouchPoints || navigator.userAgent.toLowerCase().match(/windows phone os 7/i) ? (this.each(function () { var n = !1; t(this).on("click", function (o) { var i = t(this); i[0] != n[0] && (o.preventDefault(), n = i) }), t(o).on("click touchstart MSPointerDown", function (o) { for (var i = !0, a = t(o.target).parents(), e = 0; e < a.length; e++) a[e] == n[0] && (i = !1); i && (n = !1) }) }), this) : !1 } }(jQuery, window, document);
/*! mobile menu
 */
$(document).ready(function ($) {
    //Variables
    var $wrapper = $('.full'),
        $nav = $('#main-nav > ul').clone(), //Clone just the ul directly after #main-nav
        $navigation = $('#main-nav-copy'),
        $trigger = $('#menu-trigger'),
        $nav2 = $('#account-nav > ul').clone(),
        $navigation2 = $('#account-nav-copy'),
        $trigger2 = $('#account-trigger');
    //Cloning data from one place to another
    $navigation.html($nav);
    $navigation2.html($nav2);

    $('#main-nav-copy > ul > li:has(ul)').addClass("has-sub");  //Add class if parent li has a child ul
    $('#account-nav-copy > ul > li:has(ul)').addClass("has-sub");  //Add class if parent li has a child ul
    $('.has-sub > a').wrap("<span></span>").after('<span class="open-dd"></span>'); //Add a span after the link for people to click to open the menu, so if they click the link it will function as a link

    $('.has-sub').children('span').children('.open-dd').on('click', function (event) {
        event.preventDefault();
        //If .has-sub has span > span.open-dd of children, then go up to parent span and go to next element and toggle class active and toggle a slide.
        //end the chain
        //When that accordion is opening, look back at the parent of the element with a class of .has-sub, and look at .has-sub's siblings to see if they, too, have children of span > span.open-dd, and if they do, go back to the parent span, go to the next element and remove the active class and slide it close
        //This logic is crazy, I will revisit to see if there's a better way
        $(this).parent('span').next().toggleClass('active').slideToggle('slow', function () {
            $(this).animate({
                scrollTop: $(this).offset() //Take to top when opening a long menu, this needs some work but overall works well enough for now
            });
        }).end().parent('.has-sub').siblings('.has-sub').children('span').children('.open-dd').parent('span').next().removeClass('active').slideUp(500);
    });

    //Open-close main menu panel
    $trigger.on('click', function (event) {
        event.preventDefault();
        //Close any other panel that's open, if you have more than 2 panels just use .add() to list them without creating new lines
        if ($navigation2.hasClass('open-menu')) {
            $navigation2.removeClass('open-menu'),
            $trigger2.removeClass('is-clicked');
        }
        //Close main menu panel
        $trigger.toggleClass('is-clicked');
        $navigation.toggleClass('open-menu');
    });

    //Open-close shop panel
    $trigger2.on('click', function (event) {
        event.preventDefault();
        //Close any other panel that's open, if you have more than 2 panels just use .add() to list them without creating new lines
        if ($navigation.hasClass('open-menu')) {
            $navigation.removeClass('open-menu'),
            $trigger.removeClass('is-clicked');
        }
        //Close shop panel
        $trigger2.toggleClass('is-clicked');
        $navigation2.toggleClass('open-menu');
    });

    //Close menu by clicking outside the panel
    $wrapper.on('click', function (event) {
        if (!$(event.target).is($trigger, $trigger2)) { //If your click target isn't the link that opens any of the panels then do this
            $trigger.add($trigger2).removeClass('is-clicked');
            $navigation.add($navigation2).removeClass('open-menu');
        }
    });
})
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
/*!
 * sticky-footer v4.2.0: Responsive sticky footers
 * (c) 2016 Chris Ferdinandi
 * MIT License
 * http://github.com/cferdinandi/sticky-footer
 */

(function (root, factory) {
	if ( typeof define === 'function' && define.amd ) {
		define([], factory(root));
	} else if ( typeof exports === 'object' ) {
		module.exports = factory(root);
	} else {
		root.stickyFooter = factory(root);
	}
})(typeof global !== 'undefined' ? global : this.window || this.global, (function (root) {

	'use strict';

	//
	// Variables
	//

	var stickyFooter = {}; // Object for public APIs
	var supports = 'querySelector' in document && 'addEventListener' in root; // Feature test
	var settings, wrap, footer, eventTimeout;

	// Default settings
	var defaults = {
		selectorWrap: '[data-sticky-wrap]',
		selectorFooter: '[data-sticky-footer]',
		callback: function () {}
	};


	//
	// Methods
	//

	/**
	 * Merge two or more objects. Returns a new object.
	 * @private
	 * @param {Boolean}  deep     If true, do a deep (or recursive) merge [optional]
	 * @param {Object}   objects  The objects to merge together
	 * @returns {Object}          Merged values of defaults and options
	 */
	var extend = function () {

		// Variables
		var extended = {};
		var deep = false;
		var i = 0;
		var length = arguments.length;

		// Check if a deep merge
		if ( Object.prototype.toString.call( arguments[0] ) === '[object Boolean]' ) {
			deep = arguments[0];
			i++;
		}

		// Merge the object into the extended object
		var merge = function (obj) {
			for ( var prop in obj ) {
				if ( Object.prototype.hasOwnProperty.call( obj, prop ) ) {
					// If deep merge and property is an object, merge properties
					if ( deep && Object.prototype.toString.call(obj[prop]) === '[object Object]' ) {
						extended[prop] = extend( true, extended[prop], obj[prop] );
					} else {
						extended[prop] = obj[prop];
					}
				}
			}
		};

		// Loop through each object and conduct a merge
		for ( ; i < length; i++ ) {
			var obj = arguments[i];
			merge(obj);
		}

		return extended;

	};

	/**
	 * Get height of the viewport
	 * @private
	 * @return {Number} Height of the viewport in pixels
	 */
	var getViewportHeight = function () {
		return Math.max( document.documentElement.clientHeight, window.innerHeight || 0 );
	};

	/**
	 * Set page wrapper height to fill viewport (minus footer height)
	 * @private
	 * @param {Element} wrap Page wrapper
	 * @param {Element} footer Page footer
	 * @param {Object} settings
	 */
	var setWrapHeight = function ( wrap, footer, settings ) {
		wrap.style.minHeight = ( getViewportHeight() - footer.offsetHeight ) + 'px';
		settings.callback(); // Run callback
	};

	/**
	 * Destroy the current initialization.
	 * @public
	 */
	stickyFooter.destroy = function () {

		if ( !settings ) return;

		// Unset styles
		document.documentElement.style.minHeight = '';
		document.body.style.minHeight = '';
		wrap.style.minHeight = '';
		window.removeEventListener( 'resize', eventThrottler, false );

		// Reset variables
		settings = null;
		wrap = null;
		footer = null;
		eventTimeout = null;

	};

	/**
	 * On window scroll and resize, only run events at a rate of 15fps for better performance
	 * @private
	 * @param  {Function} eventTimeout Timeout function
	 * @param  {NodeList} wrap The content wrapper for the page
	 * @param  {NodeList} footer The footer for the page
	 * @param  {Object} settings
	 */
	var eventThrottler = function () {
		if ( !eventTimeout ) {
			eventTimeout = setTimeout((function() {
				eventTimeout = null;
				setWrapHeight( wrap, footer, settings );
			}), 66);
		}
	};

	/**
	 * Initialize Plugin
	 * @public
	 * @param {Object} options User settings
	 */
	stickyFooter.init = function ( options ) {

		// feature test
		if ( !supports ) return;

		// Destroy any existing initializations
		stickyFooter.destroy();

		// Selectors and variables
		settings = extend( defaults, options || {} ); // Merge user options with defaults
		wrap = document.querySelector( settings.selectorWrap );
		footer = document.querySelector( settings.selectorFooter );

		// Sanity check
		if ( !wrap || !footer ) return;

		// Stick footer
		document.documentElement.style.minHeight = '100%';
		document.body.style.minHeight = '100%';
		setWrapHeight( wrap, footer, settings );
		window.addEventListener( 'resize', eventThrottler, false); // Run Sticky Footer on window resize

	};


	//
	// Public APIs
	//

	return stickyFooter;

}));
jQuery(document).ready(function ($) {
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
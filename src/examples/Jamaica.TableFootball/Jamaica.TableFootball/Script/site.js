﻿$(function() {
    $(".formElement.watermark").each(function() {
        var formElement = $(this);
        var input = formElement.find("input");
        var label = formElement.find("label");
        input.focus(function() {
            label.hide();
        });
        var hideWarkmarkIfValueEntered = function() {
            if (!input.val()) {
                label.show();
            }
        }
        input.blur(hideWarkmarkIfValueEntered);
        hideWarkmarkIfValueEntered();
    });

    $("input,select,button").focus(jamaica.ui.toggleFocusClass).blur(jamaica.ui.toggleFocusClass);
    $(".relativeDate").each(jamaica.dateHandling.convertTextToRelativeDate);
    $("table.leagueTable tbody tr:odd").each(function() { $(this).addClass("odd"); });
    $("table.leagueTable tbody tr:even").each(function() { $(this).addClass("even"); });
});

jamaica = {};

jamaica.ui = {

	toggleFocusClass: function() {
		$(this).toggleClass("focus");
	}	
};

jamaica.dateHandling = {
		
	relativeDateFrom: function(date) {		
		var today = Date.today();
		if (date.compareTo(today) > 0) { return "future"; } /* shouldn't happen */
		if (date.equals(today)) { return "today"; }		
		if (date.equals(today.add(-1).day())) { return "yesterday"; }
		
		for (var days = 2; days < 7; days++) {		
			if (date.equals(today.add(-days).days())) { return days + " days ago"; }			
		}
		
		if (date.compareTo(today.add(-2).weeks()) > 0) { return "1 week ago"; }
		
		for (var week = 3; week < 5; week++) {
			if (date.compareTo(today.add(-week).weeks()) > 0) { return (week - 1) + " weeks ago"; }
		}

		return date.toString("d MMM yyyy");		
	},

    convertTextToRelativeDate: function() {
		var text = $(this).text();		
		var date = Date.parse(text);

		$(this).text(jamaica.dateHandling.relativeDateFrom(date));				
    }
};
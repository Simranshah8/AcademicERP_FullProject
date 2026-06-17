$("#tabs").tabs(); $(function () { var tabs = $("#tabs").tabs(); tabs.delegate("a.fa-times-circle", "click", function () { var panelId = $(this).closest("li").remove().attr("aria-controls"); $("#" + panelId).remove(); tabs.tabs("refresh"); tabCounter = tabCounter - 1 }) }); tabCounter = 0; var sHeight = $(window).height(); var rand = function () { return Math.random().toString(36).substr(2) }; var isOldBrowser = "true"
function addTabs(tabTitle, tabContent) {
    sHeight = $(window).height() - 5; tabId = "Tab-" + rand(); var tabs = $("#tabs").tabs(); var ul = tabs.find("ul"); $("<li class='nav-item' role='presentation'><a class='nav-link active' role='tab' aria-controls='pills-second' aria-selected='false' href='#" + tabId + "'>" + tabTitle + "</a><a href='#' class='fas fa-times-circle'></a></li>").appendTo(ul); $("<div id='" + tabId + "' class='embed-responsive embed-responsive-1by1'><iframe id='Frm_" + tabId + "' src='" + tabContent + "' width='100%' class='embed-responsive-item'></iframe></div>").appendTo(tabs); jQuery("#Frm_" + tabId).bind('load', function () {
        if (isOldBrowser == "true") { }
        document.getElementById("tabs").style.cursor = "default"
    }); if (tabTitle == "Home") { tabs.find('span.ui-icon-close').remove() }
    tabs.tabs("refresh"); $("#tabs").tabs({ active: tabCounter }); tabCounter++
}
var appVersion = navigator.appVersion.substr(0, 4)
if (parseFloat(appVersion) >= 5) { var tabs = $("#tabs").tabs({ heightStyle: "screen" }) }
function confirmResponse(x) {
    if (x) {
        var frameTopBanner = parent.topbanner
        frameTopBanner.reLoadTime(); window.location.reload()
    }
}
function refreshActiveTab() { var curTab = $("#tabs").tabs('option', 'active'); var element = $("#tabs").find(".ui-tabs-panel")[curTab]; var activeTabID = ($(element).attr("id")); $("#Frm_" + activeTabID).attr("src", $("#Frm_" + activeTabID).attr("src")) }
function getActiveTabID() { var rTab = $("#tabs").tabs('option', 'active'); var element = $("#tabs").find(".ui-tabs-panel")[curTab]; var activeTabID = ($(element).attr("id")); return activeTabID }
function closeActiveTab() { tabID = getActiveTabID(); $('#tabs').tabs(); $('#tabs a[href="#' + tabID + '"]').click(); $("#tabs").tabs("refresh") }
function resizeIframe(frm) { height = screen.availHeight; document.getElementById(frm).style.height = height + "px" }; function openMenuWindow(title, arg) { addTabs(title, arg) }
$(document).ready(function () { }); function GetDataTableForExport() { var curTab = $("#tabs").tabs('option', 'active'); var element = $("#tabs").find(".ui-tabs-panel")[curTab]; var activeTabID = ($(element).attr("id")); var frm = document.getElementById("Frm_" + activeTabID).contentWindow; var tbl = frm.document.getElementById("datatable"); return tbl }
function GetPVTDataTableForExport() { var tbl = document.getElementById("pvtDatatable"); return tbl }
function printHtml(html) { var mywindow = window.open(); mywindow.document.write('<html><head><title>Schooling</title>'); mywindow.document.write('</head><body>'); mywindow.document.write(html); mywindow.document.write('</body></html>'); mywindow.print(); mywindow.close(); return !0 }
function NewLink() { alert('new link') }
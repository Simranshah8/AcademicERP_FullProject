
app.factory('Excel', function ($window) {
	var uri = 'data:application/vnd.ms-excel;base64,',
		template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>',
		base64 = function (s) { return $window.btoa(unescape(encodeURIComponent(s))); },
		format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) };
	return {
		tableToExcel: function (tableId, worksheetName) {
			var table = $(tableId),
				ctx = { worksheet: worksheetName, table: table.html() },
				href = uri + base64(format(template, ctx));
			return href;
		}
	};
})
app.controller('MyCtrl', function ($scope,Excel, $timeout) {
		$scope.exportToExcel = function (tableId) { // ex: '#my-table'
			$scope.exportHref = Excel.tableToExcel(tableId, 'Export');
			$timeout(function () { location.href = $scope.exportHref; }, 100); // trigger download
	}

	$scope.ExportCSV = function (W) {
		$(W).tableHTMLExport({
			type: 'csv',
			filename: 'Export.csv'
		});
		
	}
	$scope.Exportpdf = function (W) {
		html2canvas(document.getElementById(W), {
			onrendered: function (canvas) {
				var data = canvas.toDataURL();
				var docDefinition = {
					content: [{
						image: data,
						width: 500,

					}]
				};
				pdfMake.createPdf(docDefinition).download("Export.pdf");
			}
		});
	}
	$scope.ExportWord = function (Word) {
		var html = angular.element(document.querySelector(Word))[0].innerHTML;
		var header = "<html xmlns:o='urn:schemas-microsoft-com:office:office' " +
			"xmlns:w='urn:schemas-microsoft-com:office:word' " +
			"xmlns='http://www.w3.org/TR/REC-html40'>" +
			"<head><meta charset='utf-8'><title>Export Table to Word</title></head><body>";
		var footer = "</body></html>";
		var sourceHTML = header + "<table border='1' cellpadding='1' cellspacing='1'>" + html + "</table>" + footer;
		if (navigator.msSaveBlob) { // IE 10+
			navigator.msSaveBlob(new Blob([sourceHTML], { type: 'application/vnd.ms-word' }), "Export.doc");
		} else {
			var source = 'data:application/vnd.ms-word;charset=utf-8,' + encodeURIComponent(sourceHTML);
			var fileDownload = document.createElement("a");
			document.body.appendChild(fileDownload);
			fileDownload.href = source;
			fileDownload.download = 'Export.doc';
			fileDownload.click();
			document.body.removeChild(fileDownload);
		}
	}


	//$scope.createPDF = function () {
	//	var sTable = document.getElementById('tab').innerHTML;

	//	var style = "<style>";
	//	style = style + "table {width: 100%;font: 17px Calibri;}";
	//	style = style + "table, th, td {border: solid 1px #DDD; border-collapse: collapse;";
	//	style = style + "padding: 2px 3px;text-align: center;}";
	//	style = style + "</style>";

	//	// CREATE A WINDOW OBJECT.
	//	var win = window.open('', '', 'height=700,width=700');

	//	win.document.write('<html><head>');
	//	win.document.write('<title>Profile</title>');   // <title> FOR PDF HEADER.
	//	win.document.write(style);          // ADD STYLE INSIDE THE HEAD TAG.
	//	win.document.write('</head>');
	//	win.document.write('<body>');
	//	win.document.write(sTable);         // THE TABLE CONTENTS INSIDE THE BODY TAG.
	//	win.document.write('</body></html>');

	//	win.document.close(); 	// CLOSE THE CURRENT WINDOW.

	//	win.print();    // PRINT THE CONTENTS.

	//	linkElement.setAttribute('href', url);
	//	linkElement.setAttribute("download", win);


	//}
});

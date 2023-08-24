var API_EXCEL = 'http://localhost:5222/api/Excel';
var API_PRODUCT = 'http://localhost:5222/api/Product';
var stds;

$(document).ready(function () {
    $('#btnUpload').click(function (e) {
        e.preventDefault();
        showSpinner();
        var fileInput = $('#formFile')[0];
        if (fileInput.files.length <= 0) {
            hideSpinner();
            toastr.error("Nothing choosed", "ERROR");
            return;
        }
        if (!isExcelFile(fileInput.files[0])) {
            hideSpinner();
            toastr.error("Not excel file", "ERROR");
            return;
        }

        var formData = new FormData();
        formData.append('file', fileInput.files[0]);
        stds = null;
        $.ajax({
            type: "post",
            url: API_EXCEL + '/readProduct',
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                if (response.code != 200) {
                    hideSpinner();
                    toastr.error(response.message, "ERROR");
                    return;
                }
                $(fileInput).val('');
                setDataTable(response.data);
                hideSpinner();
                toastr.success('Upload data success', 'SUCCESSFULLY!!');
                stds = response.data;
            },
            error: function (param) {
                hideSpinner();
                toastr.error('SOMETHING WENT WRONG!', 'ERROR!!');
            }
        });
    });

    $('#btnCf').click(function (e) {
        e.preventDefault();
        showSpinner();
        if (stds == null || stds.length == 0) {
            hideSpinner();
            toastr.error('Nothing to confirm', "ERROR");
            return;
        }
       
        var dt = JSON.stringify(stds);
        $.ajax({
            type: "post",
            url: API_PRODUCT + '/addRange',
            data: dt,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.code != 200) {
                    hideSpinner();
                    toastr.error(response.message, 'ERROR');
                    return;
                }
                hideSpinner();
                toastr.success('Add products successfully', 'SUCCESSFULLY!!!');
                setTimeout(() => {
                    window.location.href = "/Product/List.cshtml";
                }, 1000);
            },
            error: function () {
                hideSpinner();
                toastr.error('SOMETHING WENT WRONG!', 'ERROR!!!');
            }
        });
    });

    $('#btnTemp').click(function (e) {
        e.preventDefault();
        $.ajax({
            type: "GET",
            url: API_EXCEL + '/templateProduct',
            xhrFields: {
                responseType: 'blob'
            },
            success: function (result) {
                var blob = result;
                var downloadUrl = URL.createObjectURL(blob);
                var a = document.createElement("a");
                a.href = downloadUrl;
                a.download = "temp-Product.xlsx";
                document.body.appendChild(a);
                a.click();
            }
        });
    });
});

function isExcelFile(file) {
    var fileName = file.name;
    var fileType = file.type;

    // Check file extension
    var fileExtension = fileName.split('.').pop().toLowerCase();
    if (fileExtension === 'xls' || fileExtension === 'xlsx') {
        return true;
    }

    // Check MIME type
    if (fileType === 'application/vnd.ms-excel') {
        return true;
    }

    return false;
}

function setDataTable(data) {
    var table = $("#tableProduct");
    $(table).empty();
    data.forEach((element) => {
        var row = $("<tr></tr>");

        var tdDetail = $('<td></td>');
        let detailHref = $('<a></a>');
        $(detailHref).attr('href', '#');
        $(detailHref).text('Detail');
        $(detailHref).click(function (e) {
            e.preventDefault();
            showProduct(element);
        });
        $(tdDetail).append(detailHref);

        $(row).append('<td>' + element.id + '</td>');
        $(row).append("<td>" + element.productName + "</td>");
        $(row).append("<td>" + (element.dDescription == null ? '<i>Not yet</i>' : element.description) + "</td>");
        $(row).append("<td>" + element.quantity + "</td>");
        $(row).append("<td>" + element.price + "</td>");
        $(row).append("<td>" + element.profitMoney + "</td>");
        
        var act = $("<span></span>");
        if (element.active) {
            $(act).text("Active");
            $(act).addClass("badge bg-success");
        } else {
            $(act).text("Deactive");
            $(act).addClass("badge bg-danger");
        }
        var r = $('<td></td>');
        $(r).append(act);
        $(row).append(r);

        $(table).append(row);
    });
}
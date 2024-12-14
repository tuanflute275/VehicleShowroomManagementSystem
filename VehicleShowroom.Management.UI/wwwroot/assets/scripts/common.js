function printElement(divElement) {
    bootbox.confirm('Do you want to download the file in this format ?', function (result) {
        if (result) {
            const element = document.getElementById(divElement);
            const opt = {
                margin: [0.5, 0.5, 0.5, 0.5],
                image: { type: 'jpeg', quality: 0.98 },
                filename: 'output.pdf',
                html2canvas: {
                    scale: 2
                },
                jsPDF: {
                    unit: 'in',
                    format: 'a4',
                    orientation: 'portrait'
                }
            };

            html2pdf().from(element).set(opt).output('blob').then(function (blob) {
                var blobURL = URL.createObjectURL(blob);
                var newTab = window.open(blobURL);
                setTimeout(function () {
                    newTab.print();
                }, 500);
            }).catch(function (error) {
                console.log('html2pdf error:', error);
            });
        }
    });
}

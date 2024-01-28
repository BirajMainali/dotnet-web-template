const __ = document.querySelector.bind(document);
const _a = document.querySelectorAll.bind(document);

$(() => {
    const pickers = $(".mb-nep-date-picker");
    const nepDatePickerHiddenElms = $(".mb-nep-date-picker-hidden-elm");

    pickers.nepaliDatePicker({
        dateFormat: "%y-%m-%d",
        closeOnDateSelect: true
    });

    pickers.on('dateChange', function (eventData) {
        const actualElm = __(`[name=${eventData.currentTarget.dataset.actualName}]`);
        if (!!actualElm) {
            const date = eventData.datePickerData.adDate;
            let value = "";
            if (date) {
                value = `${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()}`;
            }
            actualElm.value = value;
        }
    })

    nepDatePickerHiddenElms.on('change', (e) => {
        const value = e.target.value;
        if (!value) {
            return;
        }
        const displayElm = __(`[name='__${e.target.name}']`);
        const parts = value.replaceAll('/', '-').split("-");
        const {bsYear, bsMonth, bsDate} = calendarFunctions.getBsDateByAdDate(+parts[0], +parts[1], +parts[2]);
        displayElm.value = `${bsYear}-${bsMonth.toString().padStart(2, '0')}-${bsDate.toString().padStart(2, '0')}`;
    });
});
$('.tableRow').click(function () {
    $(this).nextUntil('tr.tableRow').slideToggle(0);
});

$(function () {
    for (let i = 0; i < $(".sensorsCount").html(); i++) {
        let length = 300;
        let data = [], totalPoints = 300;
        while (length--) data.push(0);

        let finalMinString = ".min" + i;
        let minValue = parseInt($(finalMinString).html());
        let finalMaxString = ".max" + i;
        let maxValue = parseInt($(finalMaxString).html());

        function getRandomData() {
            if (data.length >= 300)
                data = data.slice(1);

            let finalUrlString = ".url" + i;
            let finalUrl = $(finalUrlString).html();
            $.ajax({
                url: finalUrl,
                method: 'GET',
                beforeSend: function (xhr) { xhr.setRequestHeader('auth-token', '8e4c46fe-5e1d-4382-b7fc-19541f7bf3b0'); },
            }).done(function (result) {
                if (result.value == "true") {
                    data.push("1");
                } else if (result.value == "false") {
                    data.push("0");
                } else {
                    if (result.value < minValue) {
                        data.push(minValue);
                    } else if (result.value > maxValue) {
                        data.push(maxValue);
                    } else {
                        data.push(result.value);
                    }
                }
                let finalValueString = ".value" + i;
                $(finalValueString).text(result.value)
            })

            let res = [];
            for (let i = 0; i < data.length; ++i)
                res.push([i, data[i]])
            return res;
        }

        let finalUpdateInterval = ".updateInterval" + i;
        let updateInterval = 1000;
        $(finalUpdateInterval).val(updateInterval).change(function () {
            let v = $(this).val();
            if (v && !isNaN(+v)) {
                updateInterval = +v;
                if (updateInterval < 1)
                    updateInterval = 1;
                if (updateInterval > 2000)
                    updateInterval = 2000;
                $(this).val("" + updateInterval);
            }
        });

        let options = {
            series: { shadowSize: 0 },
            yaxis: { min: minValue, max: maxValue },
            xaxis: { show: false }
        };

        let finalPlaceHolderString = ".placeholder2" + i;
        let plot = $.plot($(finalPlaceHolderString), [getRandomData()], options);

        function update() {
            plot.setData([getRandomData()]);

            plot.draw();

            setTimeout(update, updateInterval);
        }

        update();
    }
});
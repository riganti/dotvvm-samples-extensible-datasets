(function () {
    const originalFetch = window.fetch;
    window.fetch = async function (url, options) {
        const response = await originalFetch(url, options);

        if (options.method == "POST" && !url.includes("/_dotvvm")) {
            const requestSize = options.body.length;
            const responseSize = (await response.clone().arrayBuffer()).byteLength;

            document.querySelector(".diag-box").innerHTML = `Last request to ${url}<br />👆 ${requestSize} / 👇 ${responseSize}`;
        }

        return response;
    };
})();
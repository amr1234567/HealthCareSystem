$(document).ready(function () {
    const $input = $('#search');
    const $suggestionsList = $('#suggestions');

    $input.on('input', function () {
        const query = $input.val().toLowerCase();
        $suggestionsList.empty(); // Clear previous suggestions

        if (query) {
            $.ajax({
                url: '/get-suggestions', // Replace with your endpoint
                method: 'GET',
                data: { query: query }, // Send the query to the server
                success: function (response) {
                    // Assuming the server returns an array of suggestions
                    const filteredSuggestions = response.filter(item =>
                        item.toLowerCase().startsWith(query)
                    );

                    filteredSuggestions.forEach(item => {
                        const $li = $('<li></li>').text(item);
                        $li.on('click', function () {
                            $input.val(item); // Set input value to selected suggestion
                            $suggestionsList.empty(); // Clear suggestions
                        });
                        $suggestionsList.append($li);
                    });
                },
                error: function () {
                    console.error('Error fetching suggestions.');
                }
            });
        }
    });

    // Close suggestions on outside click
    $(document).on('click', function (event) {
        if (!$input.is(event.target) && !$suggestionsList.is(event.target) && !$suggestionsList.has(event.target).length) {
            $suggestionsList.empty();
        }
    });
});

$(document).ready(function () {
    $(".appointment-row.active").on("click", function () {
        const appointmentId = $(this).data("appointment-id");
        window.location.href = `@Url.Action("AppointmentDetails","Clinic")/${appointmentId}`;
    })
})
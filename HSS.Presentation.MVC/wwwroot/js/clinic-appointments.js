//const addButton = document.getElementById('add-appointment-button');
//addButton.addEventListener("click", function () {
//    console.log("I Work");
//    document.getElementById('addAppointmentModel').style.display = 'flex';
//    document.getElementsByClassName("national-id-search-icon")[0].addEventListener("click", function () {
//        const nationalIdInput = document.getElementById("patientNationalId") as HTMLInputElement;
//        const nationalId = nationalIdInput.value;
//        if (nationalId && nationalId.match(/[0-9]{14}/)) {
//            fetch("/Reception/CheckNationalIdCorrect/" + nationalId).then(response => response.json())
//                .then((data: { success: boolean, name: string }) => {
//                if (data.success) {
//                    alert("Patient with this national id already exists --> " + data.name);
//                }
//            })
//        }
//    })
//})
//# sourceMappingURL=clinic-appointments.js.map
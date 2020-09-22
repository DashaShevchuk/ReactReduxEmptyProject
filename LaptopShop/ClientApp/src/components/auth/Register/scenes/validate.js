function validateFields(items) {
    const regex_phone = /^(?=\+?([0-9]{2})\s\(?([0-9]{3})\)\s?([0-9]{3})\s?([0-9]{2})\s?([0-9]{2})).{19}$/;
    const regex_password = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).{6,24}$/;
    const regex_email = /^[a-zA-Z0-9]+@[a-zA-Z0-9]+\.[A-Za-z]+$/;

    let errors = {};
    const { email, photo, phone, lastName, firstName, captchaText, password, confirmPassword } = items;

    if (!regex_phone.test(phone.trim())) errors.phone = "Не правильний формат телефону +xx (xxx) xxx xx xx!";

    if (!regex_email.test(email.trim())) errors.email = "Не правильний формат електронної пошти!";

    //if (email.trim() === "") errors.email = "Поле не може бути пустим!";

    if (photo.trim() === "") errors.photo = "Закинь фотку!";
    if (lastName.trim() === "") errors.lastName = "Поле не може бути пустим!";
    if (firstName.trim() === "") errors.firstName = "Поле не може бути пустим!";

    if (captchaText === "") errors.captchaText = "Поле не може бути пустим!";

    if (!regex_password.test(password)) errors.password = "Пароль повинен мати мінімум 6 символів, нижній і верхній регістр, та цифри!";
    if (password === "") errors.password = "Поле не може бути пустим!";

    if (confirmPassword !== password) errors.confirmPassword = "Повтор паролю не співпадає!";

    return errors;
}

export { validateFields };
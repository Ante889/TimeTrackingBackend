import Cookies from 'universal-cookie';


function setCookieToken(token) {
    const cookies = new Cookies();
    cookies.set('token', token, { path: '/', maxAge: 3600 });
}

function getCookieToken() {
    const cookies = new Cookies();
    return cookies.get('token');
}

function removeCookieToken() {
    const cookies = new Cookies();
    cookies.remove('token', { path: '/' });
}

export {
    setCookieToken,
    getCookieToken,
    removeCookieToken
};
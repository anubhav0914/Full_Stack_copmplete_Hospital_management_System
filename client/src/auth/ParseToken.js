import jwtDecode from 'jwt-decode';

export function parseUserFromToken(token) {
  const decoded = jwtDecode(token);

  return {
    id: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"],
    email: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"],
    role: decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
  };
}

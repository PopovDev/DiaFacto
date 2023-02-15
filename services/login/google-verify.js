import { OAuth2Client } from 'google-auth-library';
const cliendId = process.env.GOOGLE_CLIENT_ID;
const client = new OAuth2Client(cliendId);
const verify = async (token) => {
    const ticket = await client.verifyIdToken({
        idToken: token,
        audience: cliendId
    });
    const payload = ticket.getPayload();
    return payload;
}
export default verify;
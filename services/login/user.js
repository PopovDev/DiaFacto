import express from 'express';
import verify from './google-verify.js';
import { isUserExist, updateUser, findUserId } from './redis-mut.js';
import jsonwebtoken from 'jsonwebtoken';
import dotenv from 'dotenv';
dotenv.config(); 

const jwtKey = process.env.JWT_KEY;
const router = express.Router();
const cookieOptions = {
    httpOnly: true,
    secure: true,
    sameSite: 'none',
};
const cookieName = 'diaf-token';
router.post('/login', async (req, res) => {
    const { credential } = req.body;
    const payload = await verify(credential);
    try {
        const isExist = await isUserExist(payload.email);
        console.log(isExist);
        if (!isExist) {
            res.status(401).send('User not found');
        } else {
            await updateUser(payload.email, payload.picture);
            console.log("User found")
            const data = {
                email: payload.email,
                id: await findUserId(payload.email),
            };
            const token =  jsonwebtoken.sign(data, jwtKey, {
                expiresIn: '24h',
            });
            res.cookie(cookieName, token, cookieOptions).status(200).send('ok');
        }
    } catch (error) {
        res.status(500).send('Internal server error');
        console.log(error);
    }

    console.log(payload);
});

router.post('/logout', (req, res) => {
    res.clearCookie(cookieName, cookieOptions).status(200).send('ok');
});

export default router;
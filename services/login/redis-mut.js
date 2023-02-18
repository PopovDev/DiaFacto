import { createClient } from 'redis';
import { v4 as uuidv4 } from 'uuid';

const client = createClient({ url: process.env.REDIS_URL });
client.on('error', err => console.log('Redis Client Error', err));
await client.connect();

//user:GUID:name
//user:GUID:surname
//user:GUID:mails (set)
//user:GUID:created_at (unix timestamp)

const createUser = async (uname, surname, email) => {
    const id = uuidv4();
    await client.set(`user:${id}:name`, uname);
    await client.set(`user:${id}:surname`, surname);
    await client.sAdd(`user:${id}:mails`, email);
    await client.set(`user:${id}:created_at`, Date.now());
    console.log('User created');
    return id;
}

export const findUserId = async (email) => {
    const users = await client.keys(`user:*:mails`);
    for (const user of users) {
        const mails = await client.sMembers(user);
        if (mails.includes(email)) {
            return user.split(':')[1];
        }
    }
    return null;
}

export const updateUser = async (email, avatar_image) => {
    const id = await findUserId(email);
    if (id) {
        await client.set(`user:${id}:avatar_image`, avatar_image);
        return id;
    }
    throw new Error('User not found');
};

export const isUserExist = async (email) => {
    const id = await findUserId(email);
    return id !== null;
}


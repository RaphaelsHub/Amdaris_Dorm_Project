import React, { useState, useEffect, useRef } from "react";
import * as signalR from "@microsoft/signalr";
import axios from "axios";
import './Chat.css';

const Chat = () => {
  const [connection, setConnection] = useState(null);
  const [message, setMessage] = useState("");
  const [messages, setMessages] = useState([]);
  const [username, setUserName] = useState("");

  const messagesEndRef = useRef(null);

  useEffect(() => {
    const token = localStorage.getItem('token');
        
        if (!token) {
            console.error("No token found.");
            return;
        }

    const fetchUserName = async () => {
      try {
        const response = await axios.get("http://localhost:5077/api/StudentProfile", {
          headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
          },
          withCredentials: true
        });
        console.log(response);

        setUserName(response.data.firstName + " " + response.data.lastName + " " + response.data.group);
      } catch (error) {
        console.error("Ошибка при получении имени пользователя:", error);
      }
    };


    const fetchMessages = async () => {
        try {
          const response = await axios.get("http://localhost:5077/api/chat/messages", {
            headers: {
              'Content-Type': 'application/json',
            },
            withCredentials: true,
          });
          
          const formattedMessages = response.data.map((msg) => ({
            user: msg.userInfo,
            message: msg.content,
            timestamp: new Date(msg.timestamp).toLocaleString()
          }));
  
          setMessages(formattedMessages);

        } catch (error) {
          console.error("Ошибка при загрузке сообщений:", error);
        }
      };
  
    fetchUserName();
    fetchMessages();
  
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl("http://localhost:5077/chat", {
        withCredentials: true
      })
      .withAutomaticReconnect()
      .build();
  
    setConnection(newConnection);
  
    newConnection
      .start()
      .then(() => {
        newConnection.on("ReceiveMessage", (user, receivedMessage) => {
          const timestamp = new Date().toLocaleString();
          setMessages((prevMessages) => [
            ...prevMessages, 
            { user, message: receivedMessage, timestamp }
            ]);
        });
      })
      .catch((err) => console.log("Error connecting to SignalR: ", err));
  
    return () => {
      newConnection.stop();
    };
  }, []);

  useEffect(() => {
    messagesEndRef.current?.scrollIntoView({ behavior: "smooth" });
  }, [messages]);



  const sendMessage = async () => {
    if (connection && message) {
      try {
        await connection.invoke("SendMessage", username, message);
        await axios.post("http://localhost:5077/api/chat/message", {
            content: message,
            userInfo: username,
          }, {
            headers: {
              'Content-Type': 'application/json',
            },
            withCredentials: true,
          });
        setMessage("");
      } catch (err) {
        console.error("Error sending message: ", err);
      }
    }
  };

  
  



  return (
    <div className="chat-container">
      <h2 className="chat-header">Global Chat</h2>

      <div className="messages-container">
        <ul>
          {messages.map((msg, index) => (
            <li key={index} className={msg.user === username ? "my-message" : "other-message"}>
              <div className="message-bubble">
                <strong>{msg.user}</strong><br/>
                <p>{msg.message}</p>
                <em className="timestamp">{msg.timestamp}</em>
              </div>
            </li>
          ))}
        </ul>

        <div ref={messagesEndRef} />

      </div>

      <div className="input-container">

        <textarea
            className="inputChat"
          type="text"
          value={message}
          onChange={(e) => setMessage(e.target.value)}
          placeholder="Enter a message"
          rows={1}
          onKeyDown={(e) => {
            if (e.key === 'Enter' && !e.shiftKey) {
              e.preventDefault();
              sendMessage();
            }
          }}
        />

        <button className="buttonChat" onClick={sendMessage} disabled={!message}>
          {message ? (
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-send-fill" viewBox="0 0 16 16">
              <path d="M15.964.686a.5.5 0 0 0-.65-.65L.767 5.855H.766l-.452.18a.5.5 0 0 0-.082.887l.41.26.001.002 4.995 3.178 3.178 4.995.002.002.26.41a.5.5 0 0 0 .886-.083zm-1.833 1.89L6.637 10.07l-.215-.338a.5.5 0 0 0-.154-.154l-.338-.215 7.494-7.494 1.178-.471z"/>
            </svg>
          ) : (
            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-send" viewBox="0 0 16 16">
              <path d="M15.854.146a.5.5 0 0 1 .11.54l-5.819 14.547a.75.75 0 0 1-1.329.124l-3.178-4.995L.643 7.184a.75.75 0 0 1 .124-1.33L15.314.037a.5.5 0 0 1 .54.11ZM6.636 10.07l2.761 4.338L14.13 2.576zm6.787-8.201L1.591 6.602l4.339 2.76z"/>
            </svg>
          )}
        </button>
      </div>
    </div>
  );
};

export default Chat;

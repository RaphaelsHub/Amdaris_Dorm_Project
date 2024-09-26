import React, { useState, useEffect } from "react";
import * as signalR from "@microsoft/signalr";
import axios from "axios";

const Chat = () => {
  const [connection, setConnection] = useState(null);
  const [message, setMessage] = useState("");
  const [messages, setMessages] = useState([]);
  const [username, setUserName] = useState("");

  useEffect(() => {
    const fetchUserName = async () => {
      try {
        const response = await axios.get("http://localhost:5077/api/StudentProfile", {
          headers: {
            'Content-Type': 'application/json',
          },
          withCredentials: true
        });
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
          setMessages((prevMessages) => [...prevMessages, { user, message: receivedMessage }]);
        });
      })
      .catch((err) => console.log("Error connecting to SignalR: ", err));
  
    return () => {
      newConnection.stop(); // Отключение обработчика при размонтировании компонента
    };
  }, []);

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
            withCredentials: true, // Включаем отправку куки
          });
        setMessage("");
      } catch (err) {
        console.error("Error sending message: ", err);
      }
    }
  };

  return (
    <div>
      <h2>Global Chat</h2>
      <div>
        <input
          type="text"
          value={message}
          onChange={(e) => setMessage(e.target.value)}
          placeholder="Enter a message"
        />
        <button onClick={sendMessage}>Send</button>
      </div>
      <div>
        <h3>Messages</h3>
        <ul>
          {messages.map((msg, index) => (
            <li key={index}>
              <strong>{msg.user}</strong>: {msg.message}
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
};

export default Chat;

// import React, { useState, useEffect } from "react";
// import * as signalR from "@microsoft/signalr";
// import axios from "axios";

// const Chat = () => {
//   const [connection, setConnection] = useState(null);
//   const [message, setMessage] = useState("");
//   const [messages, setMessages] = useState([]);
//   const [username, setUserName] = useState("");

//   useEffect(() => {
//     const fetchUserName = async () => {
//       try {
//         const response = await axios.get("http://localhost:5077/api/StudentProfile", {
//           headers: {
//             'Content-Type': 'application/json',
//           },
//           withCredentials: true, // Включаем отправку куки
//         });
//         setUserName(response.data.firstName + " " + response.data.group);
//       } catch (error) {
//         console.error("Ошибка при получении имени пользователя:", error);
//       }
//     };

//     const fetchMessages = async () => {
//       try {
//         const response = await axios.get("http://localhost:5077/api/chat/1/messages", {
//           headers: {
//             'Content-Type': 'application/json',
//           },
//           withCredentials: true, // Включаем отправку куки
//         });
//         setMessages(response.data);
//       } catch (error) {
//         console.error("Ошибка при загрузке сообщений:", error);
//       }
//     };
  
//     fetchUserName();
//     fetchMessages();

//     const newConnection = new signalR.HubConnectionBuilder()
//       .withUrl("http://localhost:5077/chat", {
//         withCredentials: true
//       })
//       .withAutomaticReconnect()
//       .build();
  
//     setConnection(newConnection);
  
//     newConnection
//       .start()
//       .then(() => {
//         console.log("Connected to SignalR ChatHub!");
  
//         newConnection.on("ReceiveMessage", (user, receivedMessage) => {
//           setMessages((prevMessages) => [...prevMessages, { user, message: receivedMessage }]);
//         });
//       })
//       .catch((err) => console.log("Error connecting to SignalR: ", err));
  
//     return () => {
//       newConnection.stop();
//     };
//   }, []);

//   const sendMessage = async () => {
//     if (connection && message) {
//       try {
//         await axios.post("http://localhost:5077/api/chat/message", {
//           content: message,
//           userId: username,
//         }, {
//           headers: {
//             'Content-Type': 'application/json',
//           },
//           withCredentials: true, // Включаем отправку куки
//         });

//         setMessage(""); // Очистка поля сообщения после отправки
//       } catch (err) {
//         console.error("Error sending message: ", err);
//       }
//     }
//   };

//   return (
//     <div>
//       <h2>Global Chat</h2>
//       <div>
//         <input
//           type="text"
//           value={message}
//           onChange={(e) => setMessage(e.target.value)}
//           placeholder="Enter a message"
//         />
//         <button onClick={sendMessage}>Send</button>
//       </div>
//       <div>
//         <h3>Messages</h3>
//         <ul>
//           {messages.map((msg, index) => (
//             <li key={index}>
//               <strong>{msg.user}</strong>: {msg.content}
//             </li>
//           ))}
//         </ul>
//       </div>
//     </div>
//   );
// };

// export default Chat;
